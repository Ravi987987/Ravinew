﻿' REDMOND\glennha, gawd help me
'<Snippet1>
Imports System
Imports System.Reflection
Imports System.Reflection.Emit

' Compare the MSIL in this class to the MSIL
' generated by the Reflection.Emit code in class
' Example.
Public Class Sample(Of T)
    Public Field As T
    Public Shared Sub GM(Of U)(ByVal val As U)
        Dim s As New Sample(Of U)
        s.Field = val
        Console.WriteLine(s.Field)
    
    End Sub
End Class 

Public Class Example
    
    Public Shared Sub Main() 
        Dim myDomain As AppDomain = AppDomain.CurrentDomain
        Dim myAsmName As New AssemblyName("TypeBuilderGetFieldExample")
        Dim myAssembly As AssemblyBuilder = _
            myDomain.DefineDynamicAssembly(myAsmName, _
                AssemblyBuilderAccess.Save)
        Dim myModule As ModuleBuilder = _
            myAssembly.DefineDynamicModule(myAsmName.Name, _
                myAsmName.Name & ".exe")
        
        ' Define the sample type.
        Dim myType As TypeBuilder = myModule.DefineType( _
            "Sample", _
            TypeAttributes.Class Or TypeAttributes.Public)
        
        ' Add a type parameter, making the type generic.
        Dim typeParamNames() As String = { "T" }
        Dim typeParams As GenericTypeParameterBuilder() = _
            myType.DefineGenericParameters(typeParamNames)
        
        ' Define a default constructor. Normally it would 
        ' not be necessary to define the default constructor,
        ' but in this case it is needed for the call to
        ' TypeBuilder.GetConstructor, which gets the default
        ' constructor for the generic type constructed from 
        ' Sample(Of T), in the generic method GM(Of U).
        Dim ctor As ConstructorBuilder = _
            myType.DefineDefaultConstructor( _
                MethodAttributes.PrivateScope Or MethodAttributes.Public _
                Or MethodAttributes.HideBySig Or MethodAttributes.SpecialName _
                Or MethodAttributes.RTSpecialName)
        
        ' Add a field of type T, with the name Field.
        Dim myField As FieldBuilder = myType.DefineField( _
            "Field", typeParams(0), FieldAttributes.Public)
        
        ' Add a method and make it generic, with a type 
        ' parameter named U. Note how similar this is to 
        ' the way Sample is turned into a generic type. The
        ' method has no signature, because the type of its
        ' only parameter is U, which is not yet defined.
        Dim genMethod As MethodBuilder = _
            myType.DefineMethod("GM", _
                MethodAttributes.Public Or MethodAttributes.Static)
        Dim methodParamNames() As String = { "U" }
        Dim methodParams As GenericTypeParameterBuilder() = _
            genMethod.DefineGenericParameters(methodParamNames)

        ' Now add a signature for genMethod, specifying U
        ' as the type of the parameter. There is no return value
        ' and no custom modifiers.
        genMethod.SetSignature(Nothing, Nothing, Nothing, _
            New Type() { methodParams(0) }, Nothing, Nothing)
        
        ' Emit a method body for the generic method.
        Dim ilg As ILGenerator = genMethod.GetILGenerator()
        ' Construct the type Sample(Of U) using MakeGenericType.
        Dim SampleOfU As Type = _
            myType.MakeGenericType(methodParams(0))
        ' Create a local variable to store the instance of
        ' Sample(Of U).
        ilg.DeclareLocal(SampleOfU)
        ' Call the default constructor. Note that it is 
        ' necessary to have the default constructor for the
        ' constructed generic type Sample(Of U); use the 
        ' TypeBuilder.GetConstructor method to obtain this 
        ' constructor.
        Dim ctorOfU As ConstructorInfo = _
            TypeBuilder.GetConstructor(SampleOfU, ctor)
        ilg.Emit(OpCodes.Newobj, ctorOfU)
        ' Store the instance in the local variable; load it
        ' again, and load the parameter of genMethod.
        ilg.Emit(OpCodes.Stloc_0)
        ilg.Emit(OpCodes.Ldloc_0)
        ilg.Emit(OpCodes.Ldarg_0)
        ' In order to store the value in the field of the
        ' instance of Sample(Of U), it is necessary to have 
        ' a FieldInfo representing the field of the 
        ' constructed type. Use TypeBuilder.GetField to 
        ' obtain this FieldInfo.
        Dim FieldOfU As FieldInfo = _
            TypeBuilder.GetField(SampleOfU, myField)
        ' Store the value in the field. 
        ilg.Emit(OpCodes.Stfld, FieldOfU)
        ' Load the instance, load the field value, box it
        ' (specifying the type of the type parameter, U), 
        ' and print it.
        ilg.Emit(OpCodes.Ldloc_0)
        ilg.Emit(OpCodes.Ldfld, FieldOfU)
        ilg.Emit(OpCodes.Box, methodParams(0))
        Dim writeLineObj As MethodInfo = _
            GetType(Console).GetMethod("WriteLine", _
                New Type() {GetType(Object)})
        ilg.EmitCall(OpCodes.Call, writeLineObj, Nothing)
        ilg.Emit(OpCodes.Ret)
        
        ' Emit an entry point method; this must be in a
        ' non-generic type.
        Dim dummy As TypeBuilder = _
            myModule.DefineType("Dummy", _
                TypeAttributes.Class Or TypeAttributes.NotPublic)
        Dim entryPoint As MethodBuilder = _
            dummy.DefineMethod("Main", _
                MethodAttributes.Public Or MethodAttributes.Static, _
                Nothing, Nothing)
        ilg = entryPoint.GetILGenerator()
        ' In order to call the static generic method GM, it is
        ' necessary to create a constructed type from the 
        ' generic type definition for Sample. This can be ANY
        ' constructed type; in this case Sample(Of Integer)
        ' is used.
        Dim SampleOfInt As Type = _
            myType.MakeGenericType(GetType(Integer))
        ' Next get a MethodInfo representing the static generic
        ' method GM on type Sample(Of Integer).
        Dim SampleOfIntGM As MethodInfo = _
            TypeBuilder.GetMethod(SampleOfInt, genMethod)
        ' Next get a MethodInfo for GM(Of String), which is the 
        ' instantiation of generic method GM that is called
        ' by Sub Main.
        Dim GMOfString As MethodInfo = _
            SampleOfIntGM.MakeGenericMethod(GetType(String))
        ' Finally, emit the call. Push a string onto
        ' the stack, as the argument for the generic method.
        ilg.Emit(OpCodes.Ldstr, "Hello, world!")
        ilg.EmitCall(OpCodes.Call, GMOfString, Nothing)
        ilg.Emit(OpCodes.Ret)
        
        myType.CreateType()
        dummy.CreateType()
        myAssembly.SetEntryPoint(entryPoint)
        myAssembly.Save(myAsmName.Name & ".exe")
        
        Console.WriteLine(myAsmName.Name & ".exe has been saved.")
    
    End Sub 
End Class 
'</Snippet1>