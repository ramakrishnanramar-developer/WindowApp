using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DBL
{
    public static class UserSession
    {
        public static int Id;
        public static string Name;
        public static bool IsAdmin;
    }
    public class Constants
    {
        public const string EasternTimeZone = "Eastern Standard Time";
        public const string FileProcessLocation = "C:\\file_process_location";

        #region Roles
        public const string User = "User";
        public const string Admin = "admin";
        public const string SuperAdmin = "superadmin";
        public const string AdminOrSuperAdmin = Admin + "," + SuperAdmin;
        public const string AdminOrSuperOrUserAdmin = Admin + "," + SuperAdmin + "," + User;
        #endregion
        public const string Audience = "Audience";
        public const string RoleAuthToken = "Authorization";
        public const string UserId = "UserId";
        public const string UserData = "UserData";
    }
    public class SlideTypes
    {
        public static string WordCloud = "Word Cloud";
        public static string OpenEnded = "Open Ended";
        public static string Scales = "Scales";
        public static string Ranking = "Ranking";
        public static string QA = "QA";
        public static string MultipleChoice = "Multiple Choice";
        public static string TruthOrLie = "Truth or Lie";
        public static string SelectAnswer = "Select Answer";
        public static string LeaderBoard = "Leader Board";
        public static string Grid = "Grid";
        public static string TimeLine = "Timeline";
        public static string Arrows = "Arrows";
        public static string Loop = "Loop";
        public static string Breathe = "Breather";
        public static string Timer = "Timer";
        public static string HundredPoints = "100 points";
        public static string TypeAnswers = "Type Answer";
        public static string QuickForm = "Quick Form";
        public static string TwoGrid = "2 x 2 Grid";
        public static string SpinTheWheel = "Spin the Wheel";
        public static string Miro = "Miro";
        public static string DrumRoll = "Drum Roll";
        public static string Scale = "Scales";
        public static string GuessTheNumbers = "Guess the Number";
        public static string PinOnImage = "Pin on Image";
    }
    public class ThemeSettings
    {
        public static string ThemesLogo = "";
        public static string ThemesFontColor = "#252b36";
        public static string ThemesFonts = "Kanit,sans-serif";
        public static string ThemesBackgroundImage = "";
        public static string ThemesBackgroundColor = "#ffffff";
        public static string LineClour = "#252b36";
        public static double BackgroundColorOpacity = 0.55;
      
    }
    public class SlideThemeSettings
    {
        public static string ThemesLogo = "";
        public static string ThemesFontColor = "";
        public static string ThemesFonts = "#000000";
        public static string ThemesBackgroundImage = "";
        public static string ThemesBackgroundColor = "#ffffff";
        public static string LineClour = "#000000";
        public static double BackgroundColorOpacity = 0.55;
       
    }
    public class SpecialCharacters
    {
        public static string SpecialCharacter = "!#*&!$";
    }
}
