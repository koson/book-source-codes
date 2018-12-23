<configuration>
   <runtime>
      <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
         <probing privatePath="bin;binother\sub_bin "/>
         <dependentAssembly>
            <assemblyIdentity name="yourAssembly"
                              publickeytoken="23ab4ba49e0a69a1"
                              culture="en-us" />
            <bindingRedirect oldVersion="1.0.0.0"
                             newVersion="2.0.0.0"/>
         </dependentAssembly>
      </assemblyBinding>
   </runtime>
</configuration>
