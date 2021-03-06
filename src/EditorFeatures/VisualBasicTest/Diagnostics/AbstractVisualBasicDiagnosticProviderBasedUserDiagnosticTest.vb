﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports Microsoft.CodeAnalysis.Editor.UnitTests.Workspaces
Imports Microsoft.CodeAnalysis.Editor.UnitTests.Diagnostics
Imports Microsoft.CodeAnalysis.Editor.UnitTests.CodeActions

Namespace Microsoft.CodeAnalysis.Editor.VisualBasic.UnitTests.Diagnostics

    Partial Public MustInherit Class AbstractVisualBasicDiagnosticProviderBasedUserDiagnosticTest
        Inherits AbstractDiagnosticProviderBasedUserDiagnosticTest

        Private ReadOnly _compilationOptions As VisualBasicCompilationOptions =
            New VisualBasicCompilationOptions(OutputKind.ConsoleApplication).WithOptionInfer(True).WithParseOptions(New VisualBasicParseOptions(LanguageVersion.Latest))

        Protected Overrides Function GetScriptOptions() As ParseOptions
            Return TestOptions.Script
        End Function

        Protected Overrides Function CreateWorkspaceFromFile(initialMarkup As String, parameters As TestParameters) As TestWorkspace
            Return TestWorkspace.CreateVisualBasic(
                initialMarkup,
                parameters.parseOptions,
                If(parameters.compilationOptions, New VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary)))
        End Function

        Friend Overloads Async Function TestAsync(
                initialMarkup As XElement, expected As XElement, Optional index As Integer = 0) As Threading.Tasks.Task
            Dim initialMarkupStr = initialMarkup.ConvertTestSourceTag()
            Dim expectedStr = expected.ConvertTestSourceTag()

            Await MyBase.TestAsync(initialMarkupStr, expectedStr,
                                   parseOptions:=_compilationOptions.ParseOptions, compilationOptions:=_compilationOptions,
                                   index:=index)
        End Function

        Protected Overloads Async Function TestMissingAsync(initialMarkup As XElement) As Threading.Tasks.Task
            Dim initialMarkupStr = initialMarkup.ConvertTestSourceTag()

            Await MyBase.TestMissingAsync(initialMarkupStr, New TestParameters(parseOptions:=Nothing, compilationOptions:=_compilationOptions))
        End Function

        Protected Overrides Function GetLanguage() As String
            Return LanguageNames.VisualBasic
        End Function

        Friend ReadOnly Property RequireArithmeticBinaryParenthesesForClarity As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.RequireArithmeticBinaryParenthesesForClarity
            End Get
        End Property

        Friend ReadOnly Property RequireRelationalBinaryParenthesesForClarity As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.RequireRelationalBinaryParenthesesForClarity
            End Get
        End Property

        Friend ReadOnly Property RequireOtherBinaryParenthesesForClarity As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.RequireOtherBinaryParenthesesForClarity
            End Get
        End Property

        Friend ReadOnly Property IgnoreAllParentheses As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.IgnoreAllParentheses
            End Get
        End Property

        Friend ReadOnly Property RemoveAllUnnecessaryParentheses As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.RemoveAllUnnecessaryParentheses
            End Get
        End Property

        Friend ReadOnly Property RequireAllParenthesesForClarity As IOptionsCollection
            Get
                Return ParenthesesOptionsProvider.RequireAllParenthesesForClarity
            End Get
        End Property
    End Class
End Namespace
