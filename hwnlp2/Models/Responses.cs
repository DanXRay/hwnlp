using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwnlp2.Models
{
    public static class Responses
    {
        public static string GetARandomResponse()
        {
            Random random = new Random();
            int num = random.Next(_responses.Length - 1);
            return _responses[num];
        }

        private static string[] _responses =  {
            $"uhm, what? {Emoticons.Winking}",
            $"i am not a real person. {Emoticons.SpaceInvader}",
            $"i don't think i understand. {Emoticons.Dizzy}",
            $"??? {Emoticons.Thinking}",
            $"i am just a robot. {Emoticons.SpaceInvader}",
            $"does not compute. {Emoticons.Thinking}"
            };
    }
}
