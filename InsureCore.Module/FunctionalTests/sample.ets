;#DropDB InsureCoreEasyTest

#Application InsureCore.Win
#Application InsureCore.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef InsureCore.Win

*Action About...
 
*Action OK

#EndIf

#IfDef InsureCore.Web
*Action Navigation(My Details)
#EndIf
