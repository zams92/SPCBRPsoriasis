;#DropDB SPCBRPsoriasisEasyTest

#Application SPCBRPsoriasis.Win
#Application SPCBRPsoriasis.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef SPCBRPsoriasis.Win

*Action About...
 
*Action OK

#EndIf

#IfDef SPCBRPsoriasis.Web

*Action Choose Theme(Black Glass)

#EndIf
