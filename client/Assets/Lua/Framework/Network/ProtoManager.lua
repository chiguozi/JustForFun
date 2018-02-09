local table = table
local ProtoManager = {}
local _descriptorMap = {}
local _msgMap = {}
local function AddField(self,name,type,loop)
	local fieldDescriptor ={name=name,type=type,loop =loop}
	self[#self + 1] = fieldDescriptor
	self[fieldDescriptor.name] = fieldDescriptor
end
function ProtoManager.Descriptor(name)
	local descriptor = {AddField = AddField,type=name}
	_descriptorMap[name] = descriptor
	return descriptor
end

function ProtoManager.Message(id)
	local msgDescriptor = {id=id}
	_msgMap[msgDescriptor.id] = msgDescriptor
	return msgDescriptor
end
function ProtoManager.GetMessageDescripter(msgID)
	return _msgMap[msgID]
end
--[[=======================================
--]]
local _encoderMap = {}

_encoderMap['string'] = function (writer,value,descriptor)
	writer:WriteString(value or "")
end

_encoderMap['uint64'] = function (writer,value,descriptor)
	writer:WriteULong(value or 0)
end
_encoderMap['int64'] = function (writer,value,descriptor)
	writer:WriteLong(value or 0)
end

_encoderMap['uint32'] = function (writer,value,descriptor)
	writer:WriteUInt(value or 0)
end
_encoderMap['int32'] = function (writer,value,descriptor)
	writer:WriteInt(value or 0)
end

_encoderMap['uint16'] = function (writer,value,descriptor)
	writer:WriteUShort(value or 0)
end
_encoderMap['int16'] = function (writer,value,descriptor)
	writer:WriteShort(value or 0)
end

_encoderMap['uint8'] = function (writer,value,descriptor)
	writer:WriteByte(value or 0)
end
_encoderMap['bool'] = function (writer,value,descriptor)
	if nil == value then value = false end
	writer:WriteBool(value)
end
_encoderMap['float'] = function (writer,value,descriptor)
	writer:WriteFloat(value or 0)
end
_encoderMap['double'] = function (writer,value,descriptor)
	writer:WriteDouble(value or 0)
end

local function EncodeStruct(writer,value,descriptor)
	for i=1,#descriptor do
		local fieldDescriptor = descriptor[i]
		ProtoManager.Encode(writer,value and value[fieldDescriptor.name],fieldDescriptor)
	end
end


function ProtoManager.Encode(writer,value,descriptor)
	local f = _encoderMap[descriptor.type]
	local loop = descriptor.loop
	if not f then-- 自定义类型
		descriptor = _descriptorMap[descriptor.type]
        assert(nil ~= descriptor,"[ProtoManager.Encode]未知类型:" .. descriptor.type)
		f = EncodeStruct
	end
	if loop then
		local len = nil == value and 0 or #value
		writer:WriteInt(len)
		for i=1,len do
			f(writer,value[i],descriptor)
		end
	else
		f(writer,value,descriptor)
	end
end

--[[=======================================
--]]
local _decoderMap = {}

_decoderMap['string'] = function (reader,descriptor)
	return reader:ReadString()
end

_decoderMap['uint64'] = function (reader,descriptor)
	return reader:ReadULong()
end
_decoderMap['int64'] = function (reader,descriptor)
	return reader:ReadLong()
end

_decoderMap['uint32'] = function (reader,descriptor)
	return reader:ReadUInt()
end
_decoderMap['int32'] = function (reader,descriptor)
	return reader:ReadInt()
end

_decoderMap['uint16'] = function (reader,descriptor)
	return reader:ReadUShort()
end
_decoderMap['int16'] = function (reader,descriptor)
	return reader:ReadShort()
end

_decoderMap['uint8'] = function (reader,descriptor)
	return reader:ReadByte()
end
_decoderMap['bool'] = function (reader,descriptor)
	return reader:ReadBool()
end
_decoderMap['float'] = function (reader,descriptor)
	return reader:ReadFloat()
end
_decoderMap['double'] = function (reader,descriptor)
	return reader:ReadDouble()
end

local function DecodeStruct(reader,descriptor)
	local obj = {}
	for i=1,#descriptor do
		local fieldDescriptor = descriptor[i]
		local value = ProtoManager.Decode(reader,fieldDescriptor)
		obj[fieldDescriptor.name] = value
	end
	return obj
end


function ProtoManager.Decode(reader,descriptor)
	local f = _decoderMap[descriptor.type]
	local loop = descriptor.loop
	if not f then--自定义结构
		descriptor = _descriptorMap[descriptor.type]
        assert(nil ~= descriptor,"[ProtoManager.Decode]未知类型:" .. descriptor.type)
		f = DecodeStruct
	end
	if loop then
		local arr = {}
		local len = reader:ReadInt()
		for i=1,len do
			arr:insert( f(reader,descriptor) )
		end
		return arr
	else
		return f(reader,descriptor)
	end
end
return ProtoManager