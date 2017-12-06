/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Konnetic SIP .NET API")]
[assembly: AssemblyDescription("SIP .NET API")]
[assembly: AssemblyConfiguration("RELEASE")]
[assembly: AssemblyCompany("Konnetic")]
[assembly: AssemblyProduct("Konnetic Unify SIP API")]
[assembly: AssemblyCopyright("Copyright (c) Konnetic Ltd.  All rights reserved.")]
[assembly: AssemblyTrademark("Konnetic")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyDefaultAlias("KONNETIC.SIP.dll")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e56b906e-5542-44aa-9166-e75c71ef7945")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Konnetic.SIP.UnitTests")]

//, PublicKey=0024000004800000940000000602000000240000525341310004000001000100a3e2df91096b5aab1acbd6cbbcbf8b8ec968e164a45e9cf4f0a32463c2b81f40ccfeccbdb9c4109a3519d9074d74965ae0e3f54c569064b841fc06a32719df9262afaa198b08820245107271e87f12bd3750cf04b1d8a63d2f1a81253f4955d088b955f4364d2c6dabe6a8f570be95fe556a182872ec9fcb1684f8de4016f4c1")]
[assembly: NeutralResourcesLanguageAttribute("")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
