using System;
using System.Reflection;

public class ModApi {
    
    public class Mod {
        public String Name {get; set;}
        public String Id { get; set; }
        public String Author { get; set; }
        public String Description { get; set; }
        public String Version { get; set; }
        public String StartScene { get; set; }
    }

    public static void LoadMods() {
        
    }
    
    public static void LoadMod(Mod mod) {
        Assembly.LoadFile(mod.Id + ".dll");
    }
    
}
