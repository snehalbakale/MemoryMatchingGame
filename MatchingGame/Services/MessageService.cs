using System;
using System.Collections.Generic;

namespace MatchingGame.Services
{
    public static class MessageService
    {
        private static List<string> StartScoreMessage = new List<string>();
        private static List<string> MinusScoreMessage = new List<string>();
        private static List<string> NegativeScoreMessage = new List<string>();
        private static List<string> NegativeScoreCrossMessage = new List<string>();
        private static List<string> OneMatchScoreMessage = new List<string>();
        private static List<string> TwoMatchScoreMessage = new List<string>();
        private static List<string> ThreeFourScoreMessage = new List<string>();
        private static List<string> FiveSixMatchScoreMessage = new List<string>();
        private static List<string> SevenMatchScoreMessage = new List<string>();
        private static List<string> AllMatchScoreMessage = new List<string>();
        public static string GetMessage(int score, short matchCount, bool? foundMatchFlag)
        {
            SetMessages();
            string message = string.Empty;
            Random random = new Random();
            int index;
            if (foundMatchFlag is null)
            {
                index = random.Next(StartScoreMessage.Count);
                message = StartScoreMessage[index].ToString();
                return message;
            }
            if (!foundMatchFlag.Value && score >= 0)
            {
                index = random.Next(MinusScoreMessage.Count);
                message = MinusScoreMessage[index].ToString();
                return message;
            }
            if (!foundMatchFlag.Value && score <= 0 && score >= -8)
            {
                index = random.Next(NegativeScoreMessage.Count);
                message = NegativeScoreMessage[index].ToString();
                return message;
            }
            if (!foundMatchFlag.Value && score <= -9)
            {
                index = random.Next(NegativeScoreCrossMessage.Count);
                message = NegativeScoreCrossMessage[index].ToString();
                return message;
            }
            if (matchCount == 1)
            {
                index = random.Next(OneMatchScoreMessage.Count);
                message = OneMatchScoreMessage[index].ToString();
            }
            else if (matchCount == 2)
            {
                index = random.Next(TwoMatchScoreMessage.Count);
                message = TwoMatchScoreMessage[index].ToString();
            }
            else if (matchCount == 3 || matchCount == 4)
            {
                index = random.Next(ThreeFourScoreMessage.Count);
                message = ThreeFourScoreMessage[index].ToString();
            }
            else if (matchCount == 5 || matchCount == 6)
            {
                index = random.Next(FiveSixMatchScoreMessage.Count);
                message = FiveSixMatchScoreMessage[index].ToString();
            }
            else if (matchCount == 7)
            {
                index = random.Next(SevenMatchScoreMessage.Count);
                message = SevenMatchScoreMessage[index].ToString();
            }
            else if (matchCount == 8)
            {
                index = random.Next(AllMatchScoreMessage.Count);
                message = AllMatchScoreMessage[index].ToString();
            }

            return message;
        }

        public static void SetMessages()
        {
            SetStartScoreMessages();
            SetMinusScoreMessages();
            SetNegativeScoreMessage();
            SetNegativeScoreCrossMessage();
            SetOneMatchScoreMessage();
            SetTwoMatchScoreMessage();
            SetThreeFourMatchScoreMessage();
            SetFiveSixMatchScoreMessage();
            SetSevenMatchScoreMessage();
            SetAllMatchScoreMessage();
        }

        public static void SetStartScoreMessages()
        {
            StartScoreMessage.Add("Good luck, You can do it!");
            StartScoreMessage.Add("All the best, Do your best!");
        }

        public static void SetMinusScoreMessages()
        {
            MinusScoreMessage.Add("Thats ok, go on");
            MinusScoreMessage.Add("Minus score");
        }

        public static void SetNegativeScoreMessage()
        {
            NegativeScoreMessage.Add("Oh dear, dont worry! Try to focus");
            NegativeScoreMessage.Add("Negative score");
        }

        public static void SetNegativeScoreCrossMessage()
        {
            NegativeScoreCrossMessage.Add("Oh dear, its negative");
            NegativeScoreCrossMessage.Add("Negative score is crossed! You may restart the game and try focus from beginning");
        }

        public static void SetOneMatchScoreMessage()
        {
            OneMatchScoreMessage.Add("Yuhu! Yes good going.");
            OneMatchScoreMessage.Add("One match score");
        }

        public static void SetTwoMatchScoreMessage()
        {
            TwoMatchScoreMessage.Add("Yuhu! Yes keep going.");
            TwoMatchScoreMessage.Add("Two match score");
        }
        public static void SetThreeFourMatchScoreMessage()
        {
            ThreeFourScoreMessage.Add("Yes few more! keep going.");
            ThreeFourScoreMessage.Add("Three and four match score");
        }
        public static void SetFiveSixMatchScoreMessage()
        {
            FiveSixMatchScoreMessage.Add("Yes few more! keep going.");
            FiveSixMatchScoreMessage.Add("Five six.. match score");
        }
        public static void SetSevenMatchScoreMessage()
        {
            SevenMatchScoreMessage.Add("Yes few more! keep going.");
            SevenMatchScoreMessage.Add("Seven match score");
        }
        public static void SetAllMatchScoreMessage()
        {
            AllMatchScoreMessage.Add("Yuhu, you did it.");
            AllMatchScoreMessage.Add("All match score");
        }
    }
}
