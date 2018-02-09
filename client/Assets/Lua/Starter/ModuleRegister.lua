local require= require
local ModuleEnum = ModuleEnum
local App = App

App.AddModule( ModuleEnum.Login,require("Modules/Login/LoginModule"):new() )
App.AddModule(ModuleEnum.Role,require("Modules/Role/RoleModule"):new() )
App.AddModule(ModuleEnum.Mahjong,require("Modules/Mahjong/MahjongModule"):new() )