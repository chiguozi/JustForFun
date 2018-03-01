local InputManager = {}
local EventEnum = EventEnum
local Input = Input

function InputManager.Update()
	if Input.GetMouseButtonUp(0) then
		EventManger.SendEvent(EventEnum.MouseBtnUp, Input.mousePosition)
	end
	if Input.GetMouseButtonDown(0) then
		EventManger.SendEvent(EventEnum.MouseBtnDown, Input.mousePosition)
	end
end

return InputManager