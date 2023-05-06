using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashcardLab.Utilities
{
    public static class LevelSpans
    {
        public static TimeSpan s0 = new TimeSpan(0, 0, 0);
        public static TimeSpan s1 = new TimeSpan(3, 0, 0);
        public static TimeSpan s2 = new TimeSpan(7, 0, 0);
        public static TimeSpan s3 = new TimeSpan(1, 0, 0, 0);
        public static TimeSpan s4 = new TimeSpan(2, 0, 0, 0);
        public static TimeSpan s5 = new TimeSpan(7, 0, 0, 0);
        public static TimeSpan s6 = new TimeSpan(14, 0, 0, 0);
        public static TimeSpan s7 = new TimeSpan(30, 0, 0, 0);
        public static TimeSpan s8 = new TimeSpan(4, 0, 0, 0);
        public static TimeSpan s9 = new TimeSpan(0, 0, 0);

        public static TimeSpan[] spans = new TimeSpan[10]
        {
            s0, s1, s2, s3, s4, s5, s6, s7, s8, s9
        };

        public static String[] labels = new String[10] {
        "Entry (0)",
        "Beginner (1)",
        "Elementary (2)",
        "Pre-Intermediate (3)",
        "Intermediate (4)",
        "Upper-Intermediate (5)",
        "Advanced (6)",
        "Proficient (7)",
        "Expert (8)",
        "Mastery (9)"
        };

        public static String[] colors = new String[10] {
        "#7eff5e",
        "#54ffb5",
        "#73fff1",
        "#73b2ff",
        "#697dff",
        "#865eff",
        "#ff8757",
        "#ff1c86",
        "#ff4242",
        "#171717"
        };

        public static String GetHtmlSpan(int level)
        {
            return "<span style=\'color:" + colors[level] + "\'>" + labels[level] +"</span>";
        }
    }
}