local isnull = tolua.isnull

local setmetatableindex_
setmetatableindex_ = function(t, index)
    if type(t) == "userdata" then
        local peer = tolua.getpeer(t)
        if not peer then
            peer = {}
            tolua.setpeer(t, peer)
        end
        setmetatableindex_(peer, index)
    else
        local mt = getmetatable(t)
        if not mt then mt = {} end
        if not mt.__index then
            mt.__index = index
            setmetatable(t, mt)
        elseif mt.__index ~= index then
            setmetatableindex_(mt, index)
        end
    end
end
setmetatableindex = setmetatableindex_

-- 使用new的使用是用.    xxx.new ====>  xxx:Create
function class(classname, ...)
    local cls = {__cname = classname}

    local supers = {...}
    for _, super in ipairs(supers) do
        local superType = type(super)
        assert(superType == "nil" or superType == "table" or superType == "function",
            string.format("class() - create class \"%s\" with invalid super class type \"%s\"",
                classname, superType))

        if superType == "function" then
            assert(cls.__create == nil,
                string.format("class() - create class \"%s\" with more than one creating function",
                    classname));
            -- if super is function, set it to __create
            cls.__create = super
        elseif superType == "table" then
            if super[".isclass"] then
                -- super is native class
                assert(cls.__create == nil,
                    string.format("class() - create class \"%s\" with more than one creating function or native class",
                        classname));
                cls.__create = function() return super:create() end
            else
                -- super is pure lua class
                cls.__supers = cls.__supers or {}
                cls.__supers[#cls.__supers + 1] = super
                if not cls.super then
                    -- set first super pure lua class as class.super
                    cls.super = super
                end
            end
        else

            error(string.format("class() - create class \"%s\" with invalid super type",
                        classname), 0)
        end
    end

    cls.__index = cls
    if not cls.__supers or #cls.__supers == 1 then
        setmetatable(cls, {__index = cls.super})
    else
        setmetatable(cls, {__index = function(_, key)
            local supers = cls.__supers
            for i = 1, #supers do
                local super = supers[i]
                if super[key] then return super[key] end
            end
        end})
    end

    if not cls.ctor then
        -- add default constructor
        cls.ctor = function() end
    end
    cls.new = function(...)
        local instance
        if cls.__create then
            instance = cls.__create(...)
        else
            instance = {}
        end
        setmetatableindex(instance, cls)
        -- setmetatable(instance, cls)
        instance.class = cls
        instance:ctor(...)
        return instance
    end
    cls.create = function(_, ...)
        return cls.new(...)
    end

    return cls
end


-- 带堆栈的log
function LogError(...)
	if Debugger.useLog == false then
		return
	end
	local tab = {}
	for k, v in pairs({...}) do
		table.insert(tab, tostring(v))
	end
	local str = table.concat(tab, "\t")
	local output = str ..'\n'.. debug.traceback()..'\n'
	Debugger.LogError(output)
end

-- 带堆栈的log
function Log(...)
	if Debugger.useLog == false then
		return
	end
	local tab = {}
	for k, v in pairs({...}) do
		table.insert(tab, tostring(v))
	end
	local str = table.concat(tab, "\t")
	local output = str ..'\n'.. debug.traceback()..'\n'
	Debugger.Log(output)
end


function LoadTable(t)
  if type(t) ~= "table" then 
    return t
  end 

  local tab = ""
  local strArr = {}
  table.insert(strArr, "")
  for k,v in pairs(t) do 
    if v ~= nil then 
      local key = tab
      if type(k) == "string" then
        key =  string.format("%s[\"%s\"] = ", key, tostring(k) )
      else 
        key =  string.format("%s[%s] = ", key, tostring(k) )
      end 
      
      table.insert(strArr, key)
      if type(v) == "table" then 
        table.insert(strArr, LoadTable(v) )
      elseif type(v) == "string" then 
        table.insert(strArr, string.format("\"%s\";\n",tostring(v)))
      else 
        table.insert(strArr, string.format("%s;\n",tostring(v)))
      end 
    end 
  end 
  
  local str = string.format("\n%s{\n%s%s};\n", tab, table.concat(strArr), tab)
  return str
end 

function GetTblData(...)
  local strArr = {}
  table.insert(strArr, "")
  for _,v in pairs({...}) do
    local tempType = type(v)
    if tempType == "table" then
      table.insert(strArr, LoadTable(v) )
    else
      table.insert(strArr, tostring(v) )
    end
    table.insert(strArr, " ")
  end
  
  return string.format("GAME_LOG: %s \n", table.concat(strArr))
end

function IsNil(obj)
  if obj == nil then
    return true
  end
  if isnull(obj) then
    return true
  end
  return false
end
