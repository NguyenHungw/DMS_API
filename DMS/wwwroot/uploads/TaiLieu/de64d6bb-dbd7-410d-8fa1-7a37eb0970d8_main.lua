

local Replicated = game:GetService("ReplicatedStorage")
local Player = game:GetService("Players").LocalPlayer
local PlayerGui = Player:WaitForChild("PlayerGui", 15)
local VIM = game:GetService("VirtualInputManager")

local SETTINGS = _G.RivalsSettings or {
    AUTO_OPEN = true,
    AUTO_SELL = true,
    AUTO_BUY  = true,
    CYCLE_WAIT = 3
}

warn("OpenCase: " .. (SETTINGS.AUTO_OPEN and "BAT" or "TAT"))

local CASE_1 = "StrikeCase"
local CASE_2 = "ReleaseCase"
local PURCHASE_NAME = "StrikeCase_x1"
local SELL_RARITIES = { ["Rare"] = true, ["Epic"] = true, ["Legendary"] = true }
local cycleCount = 0
local startTime = os.time()

local sg = Instance.new("ScreenGui", PlayerGui)
sg.Name = "V47_Status"
local lb = Instance.new("TextLabel", sg)
lb.Size = UDim2.new(0.4, 0, 0.08, 0)
lb.Position = UDim2.new(0.3, 0, 0.01, 0)
lb.BackgroundColor3 = Color3.new(0,0,0)
lb.BackgroundTransparency = 0.4
lb.TextColor3 = Color3.new(1,1,1)
lb.TextSize = 13
lb.RichText = true
lb.Text = "Dang khoi dong..."

local function updateHUD(gold, status)
    local elapsed = os.time() - startTime
    local openTxt = SETTINGS.AUTO_OPEN and "<font color='#00FF00'>ON</font>" or "<font color='#FF0000'>OFF</font>"
    lb.Text = string.format([[
<font color="#FFFF00">V47 [%ds][Lượt %d]</font> | Mở Hòm: %s
<font color="#00FF00">Vàng: %s</font>
Trạng thái: %s]], elapsed, cycleCount, openTxt, tostring(gold), status)
end

task.spawn(function()
    local codes = {"freecase", "3mvisit"}
    local codeRemote = Replicated:WaitForChild("Remote"):WaitForChild("Any"):WaitForChild("Code")
    for _, code in ipairs(codes) do
        pcall(function() 
            codeRemote:InvokeServer(code) 
            warn(" Da gui lenh nhap code: " .. code)
        end)
        task.wait(0.5)
    end
end)


local function getActualGold()
    local candidates = {}
    local sc = sg.AbsoluteSize
    for _, v in pairs(PlayerGui:GetDescendants()) do
        if v:IsA("TextLabel") and v.Visible and v.Text:match("%d") then
            if v.Text:find("123456789") then continue end
            local clean = v.Text:gsub("[^%d]", "")
            local val = tonumber(clean)
            if val and val > 0 then
                local weight = 0
                local pos = v.AbsolutePosition
                if pos.X > sc.X * 0.75 and pos.Y > sc.Y * 0.75 then weight = weight + 5000 end
                table.insert(candidates, {v = val, w = weight})
            end
        end
    end
    table.sort(candidates, function(a, b) return a.w > b.w end)
    return #candidates > 0 and candidates[1].v or 0
end


local function forceSyncV15()
    task.spawn(pcall, function() Replicated.Shared.BetterReplication.Interface.Remotes.GetRegistry:InvokeServer() end)
    task.spawn(pcall, function() Replicated.Remote.InstanceDataService.Request:InvokeServer("Weapon") end)
end

local function virtualTouchItemsV15()
    task.spawn(function()
        for _, v in pairs(PlayerGui:GetDescendants()) do
            if (v:IsA("TextLabel") or v:IsA("TextButton")) and v.Text:upper() == "ITEMS" then
                local target = (v:IsA("TextButton") and v) or v.Parent
                if target and target:IsA("GuiObject") then
                    local absP = target.AbsolutePosition
                    local absS = target.AbsoluteSize
                    local x = absP.X + (absS.X / 2)
                    local y = absP.Y + (absS.Y / 2) + 58
                    pcall(function() VIM:SendTouchEvent(os.time(), 0, x, y) task.wait(0.1) VIM:SendTouchEvent(os.time(), 2, x, y) end)
                    break
                end
            end
        end
    end)
end

local function runMasterCycle(inv, gold)
    local cfg = require(Replicated.Config.WeaponConfig)
    
    if SETTINGS.AUTO_OPEN then
        updateHUD(gold, "TURBO Spam mo Dual 20x...")
        for i = 1, 20 do
            task.spawn(pcall, function() Replicated.Remote.GachaService.Gacha:InvokeServer(CASE_1, 1) end)
            task.spawn(pcall, function() Replicated.Remote.GachaService.Gacha:InvokeServer(CASE_2, 1) end)
            task.wait(0.05)
        end
    end
    task.wait(0.3)

    if SETTINGS.AUTO_SELL then
        updateHUD(gold, "Dang ban đồ rác...")
        for h, item in pairs(inv) do
            if type(item) == "table" and item.WeaponName then
                local rar = cfg[item.WeaponName] and tostring(cfg[item.WeaponName].Rarity) or "Unknown"
                if rar:lower():find("mystic") then
                    task.spawn(pcall, function() Replicated.Remote.WeaponService.SetLock:InvokeServer(h, true) end)
                elseif SELL_RARITIES[rar] then
                    task.spawn(pcall, function() Replicated.Remote.WeaponService.Sell:InvokeServer({[1] = h}) end)
                    task.wait(0.05)
                end
            end
        end
    end

    -- PHASE 3: TURBO MUA HOM
    if SETTINGS.AUTO_BUY and gold >= 1000 then
        updateHUD(gold, " Spam mua hòm...")
        local currentGold = gold
        while currentGold >= 1000 do
            local p = 1
            if currentGold >= 10000 then p = 10 elseif currentGold >= 3000 then p = 3 end
            task.spawn(pcall, function() Replicated.Remote.ShopService.Purchase:InvokeServer(PURCHASE_NAME, p) end)
            currentGold = currentGold - (p * 1000)
            task.wait(0.05)
        end
    end
end

-- 4. VONG LAP MASTER
task.spawn(function()
    while true do
        cycleCount = cycleCount + 1
        updateHUD("Quet...", "Nạp du lieu ...")
        
        forceSyncV15()
        virtualTouchItemsV15()
        task.wait(4)
        
        local invFound = nil
        for _, v in pairs(getgc(true)) do
            if type(v) == "table" then
                for k, d in pairs(v) do
                    if type(k) == "string" and #k > 20 and k:sub(-2) == "==" then
                        if type(d) == "table" and d.WeaponName then invFound = v break end
                    end
                end
            end
            if invFound then break end
        end

        local currentGold = getActualGold()
        if invFound then
            runMasterCycle(invFound, currentGold)
            updateHUD(currentGold, "Xong. Nghi " .. SETTINGS.CYCLE_WAIT .. "s...")
        else
            updateHUD(currentGold, "! Khong thay kho do. Thu lai...")
        end
        
        task.wait(SETTINGS.CYCLE_WAIT)
    end
end)
