ShellGacUtil
============

This tool was inspired by reading this article on CodeProject:

[Register/Unregister .NET Asseblies into GAC Using Shell Extensions][1]

by By Moim Hossain, 25 May 2007


Unfortunately, the method used for the registration of assembly does not meet my needs so I rewrote the instrument using more conventional API, removing the dependency from a specific version of Visual Studio.

I also have simplified the level of COM interface through the use of [ShellSharp][2]
downlodable throught the NuGet Extension.
I then added a convenient setup that is a [WIX Project][3].

  [1]: http://www.codeproject.com/Articles/18938/Register-Unregister-NET-Asseblies-into-GAC-Using-S
  [2]: http://sharpshell.codeplex.com/
  [3]: http://wix.codeplex.com/

