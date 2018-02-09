local assert = assert
local List = class("List")
function List:ctor()
	self._front = 0
	self._rear 	= 0
	self._list = {}
end

function List:Count()
	return self._rear - self._front
end

function List:First()
	if self._rear == self._front then
		return nil
	end

	local item = self._list[self._front]

	return item
end

function List:Last()
	if self._rear == self._front then
		return nil
	end

	local item = self._list[self._rear]

	return item
end

function List:Enqueue(item)
	self._list[self._rear] = item
	self._rear = self._rear + 1
end

function List:Dequeue()
	if self._rear == self._front then
		return nil
	end

	local item = self._list[self._front]

	self._front = self._front + 1

	if self._front == self._rear then
		self._front = 0
		self._rear 	= 0
	end

	return item
end

function List:Push(item)
	self._list[self._rear] = item
	self._rear = self._rear + 1
end
function List:Pop()
	if self._rear == self._front then
		return nil
	end
	local item = self._list[self._rear]
	self._rear = self._rear - 1
	return item
end
function List:Add(item,index)
	local pos = self._rear
	if index then 
		assert(index > 0 and index <= self._rear - self._front,"index out of range")
		
		pos =  self._front + index -1
		
		for i=self._rear + 1, pos + 1 ,-1 do
			self._list[i] = self._list[i-1]
		end
	else
		pos = self._rear + 1
	end
	self._list[pos] = item
	self._rear = self._rear + 1
end

function List:AddRange(arr,startIndex,count,index)
	if not count then count = #arr end
	local pos
	if index then 
		assert(index > 0 and index <= self._rear - self._front,"index out of range")
		pos =  self._front + index -1
		for i=self._rear + count, pos + 1 ,-1 do
			self._list[i] = self._list[i-1]
		end
	else
		pos = self._rear
	end
	startIndex = startIndex or 1
	
	for i=1,count do
		self._list[pos  + i] = arr[startIndex + i-1]
	end
	self._rear = self._rear + count
end
local function _RemoveAt(list,len,index,count)
	local newLen = len - count
	for i=index,newLen do
		list[i] = list[i+count]
	end
	for i = newLen+1,len do
		list[i] = nil
	end
end
function List:Remove(item,removeAll)
	if removeAll then
		for i=self._rear,self._front,-1 do
			if self._list[i] == item then
				_RemoveAt(self._list,self._rear,pos,1)
				self._rear = self._rear - 1
			end
		end
	else
		for i=self._front,self._rear do
			if self._list[i] == item then
				_RemoveAt(self._list,self._rear,pos,1)
				self._rear = self._rear - 1
				return 
			end
		end
	end
end
function List:RemoveAt(index)
	if self._front == self._rear then
		return 
	end
	local pos = self._front + index -1
	if pos < self._front or pos > self._rear then 
		return
	end
	_RemoveAt(self._list,self._rear,pos,1)
	self._rear = self._rear - 1
end
function List:RemoveRange(index,count)
	local len = self._rear - self._front
	assert(index > 0 and index <= len,"index out of range")
	if count then
		assert(count >= 0 and count <= len,"count invalidate")
	else
		count = len - index
	end
	_RemoveAt(self._list,self._rear,index,count)
	self._rear = self._rear - count
end
function List:Contains(item)
	for i=self._rear,self._front,-1 do
		if self._list[i] == item then
			return true
		end
	end
end
function List:IndexOf(item)
	for i=self._front,self._rear do
		if self._list[i] == item then
			return i- self._front
		end
	end
	return 0
end
function List:GetAt(index)
	return self._list[self._front + index -1]
end
function List:SetAt(index,item)
	assert(index > 0 and index <= self._rear - self._front,"index out of range")
	self._list[self._front + index -1] = item
end
function List:Clear()
	if self._front ~= self._rear then
		for i=self._front,self._rear do
			self._list[i] = nil
		end
	end
	self._front = 0
	self._rear 	= 0
end
return List