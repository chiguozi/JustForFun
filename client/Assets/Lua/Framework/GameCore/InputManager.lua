local InputManager = {}
local EventEnum = EventEnum
local Input = UnityEngine.Input

function InputManager.Update()
	if Input.GetMouseButtonUp(0) then
		EventManager.SendEvent(EventEnum.MouseBtnUp, Input.mousePosition)
	end
	if Input.GetMouseButtonDown(0) then
		EventManager.SendEvent(EventEnum.MouseBtnDown, Input.mousePosition)
	end
end

return InputManager