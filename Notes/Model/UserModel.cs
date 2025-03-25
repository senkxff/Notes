namespace Notes.Model
{
    class UserModel
    {
        private static int currentId = 0;

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string password;
        public string Password
        {
            set { password = value; }
        }

        public UserModel(string name, string password)
        {
            this.id = currentId++;
            this.name = name;
            this.password = password;
        }
    }
}
