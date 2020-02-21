using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matilde.Models
{
    public class Emoticons
    {
        /*
          source: https://emojipedia.org/people/
        */
        public static string Happy = "😀";
        public static string Winking = "😉";
        public static string Sad = "🙁";
        public static string OK = "👌";
        public static string Thermometer = "🤒";
        public static string Thinking = "🤔";
        public static string Doc = "👩‍⚕️";
        public static string Astronaut = "👩‍🚀";
        public static string Vulcan = "🖖";
        public static string SpaceInvader = "👾";
        public static string Dizzy = "😵";
        public static string doc12 = "👌";
        public static string do1c3 = "👌";
        public static string doc14 = "👌";
        public static string doc15 = "👌";
        public static string doc16 = "👌";

        public static string EmoticonifyTheConfidence(int confidence)
        {
            if (confidence > 49)
            {
                return string.Empty;
                return Emoticons.Doc;
            }
            if (confidence < 20)
            {
                return Responses.GetARandomResponse();
            }
            return Emoticons.Thinking;
        }
        public static string EmoticonFromAspect(string aspectString)
        {
            string checkMe = aspectString.Substring(0, 9) ?? aspectString;
            switch (checkMe)
            {
                case "howToPrep":
                    return string.Empty;
                    return Emoticons.Astronaut;
                case "whatIs":
                    return Emoticons.Happy;
                case "signsAndS":
                    return Emoticons.Sad;
                case "decision*":
                    return Emoticons.Thinking;
                case "whatCause":
                    return Emoticons.Doc;
                case "treatment":
                    return Emoticons.Thermometer;


                default:
                    return $"({checkMe}:{aspectString})";
            }
        }
    }

    

}
