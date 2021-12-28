namespace Application.Services
{
    public static class MetricsGrade
    {
        public static string FirstContentfulPaintGrade(int value)
        {
            var result = value switch
            {
                > 1600 => "F",
                > 1205 => "D",
                > 934 => "C",
                _ => "A",
            };
            return result;
        }

        public static string FirstContentfulPaintDescription(int value)
        {
            var result = value switch
            {
                > 1600 => "Dużo więcej niż powinno",
                > 1205 => "Więcej niż powinno",
                > 934 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }

        public static string SpeedIndexGrade(int value)
        {
            var result = value switch
            {
                > 2300 => "F",
                > 1711 => "D",
                > 1311 => "C",
                _ => "A",
            };
            return result;
        }

        public static string SpeedIndexDescription(int value)
        {
            var result = value switch
            {
                > 2300 => "Dużo więcej niż powinno",
                > 1711 => "Więcej niż powinno",
                > 1311 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }

        public static string LargestContentfulPaintGrade(int value)
        {
            var result = value switch
            {
                > 2400 => "F",
                > 1666 => "D",
                > 1200 => "C",
                _ => "A",
            };
            return result;
        }

        public static string LargestContentfulPaintDescription(int value)
        {
            var result = value switch
            {
                > 2400 => "Dużo więcej niż powinno",
                > 1666 => "Więcej niż powinno",
                > 1200 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }

        public static string TimeToInteractiveGrade(int value)
        {
            var result = value switch
            {
                > 4500 => "F",
                > 3280 => "D",
                > 2468 => "C",
                _ => "A",
            };
            return result;
        }

        public static string TimeToInteractiveDescription(int value)
        {
            var result = value switch
            {
                > 4500 => "Dużo więcej niż powinno",
                > 3280 => "Więcej niż powinno",
                > 2468 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }

        public static string TotalBlockingTimeGrade(int value)
        {
            var result = value switch
            {
                > 350 => "F",
                > 224 => "D",
                > 150 => "C",
                _ => "A",
            };
            return result;
        }

        public static string TotalBlockingTimeDescription(int value)
        {
            var result = value switch
            {
                > 350 => "Dużo więcej niż powinno",
                > 224 => "Więcej niż powinno",
                > 150 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }

        public static string FullyLoadedTimeGrade(int value)
        {
            var result = value switch
            {
                > 12000 => "F",
                > 8000 => "D",
                > 4000 => "C",
                _ => "A",
            };
            return result;
        }

        public static string FullyLoadedTimeDescription(int value)
        {
            var result = value switch
            {
                > 12000 => "Dużo więcej niż powinno",
                > 8000 => "Więcej niż powinno",
                > 4000 => "Ok, ale wymaga poprawy",
                _ => "Świetnie, nic do poprawy",
            };
            return result;
        }
    }
}
