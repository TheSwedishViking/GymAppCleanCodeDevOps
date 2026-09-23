namespace GymSwipe.ExampleData
{
    public class userLoginTest
    {

        public static UserClass ExampleData()
        {
            string name = "Hugh Jackman";
            int age = 58;
            bool athelte = true;
            int height = 185;

            UserClass currentUser = new()
            {
                Age = age,
                Name = name,
                Athlete = athelte,
                Height = height
            };

            return currentUser;
        }





    }
}
