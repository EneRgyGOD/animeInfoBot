using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace animeInfoBot
{
    public class Commands 
    { 
    
        public string CommandChooser(string input) 
        {
            string output = "";
            if (input == "/info") output = CmdInfo();
            return output;
        }
        public string CmdInfo() 
        {
            return "Бот ще в розробці тому прошу підіть випісяйтесь поки він робиться. Це буде достатньо довго тому що у автора приступ даунізму та і взгалаі він лінивий, але якщо ви хочете, що б він запрацював дайте 3 гривні на сирок пж";
        }

    }
}
