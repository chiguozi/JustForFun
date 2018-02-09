Utils = {}

function Utils.SetTrLayer(tr, layer)
	if tr == nil then
		return
	end
	tr.gameObject.layer = layer
	for i = 1, tr.childCount do
		local child = tr:GetChild(i - 1)
		Utils.SetTrLayer(child, layer)
	end
end


function Utils.CopyVector3(x, y, z, to)
	to.x = x or to.x
	to.y = y or to.y
	to.z = z or to.z
end

