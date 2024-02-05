using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DynamicNavigationMenu.Models
{
    public class Repository:DbContext
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration configuration { get; set; }

        private string GetConnectionStringg()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configuration = builder.Build();
            return configuration.GetConnectionString("DbConn");
        }

        public List<NavigationMenuList> getAllMainMenus()
        {
            List<NavigationMenuList> _MainMenuLists = new List<NavigationMenuList>();

           
            using (_connection = new SqlConnection(GetConnectionStringg()))
            {
                try
                {
                    _command = _connection.CreateCommand();

                    _command.CommandText = "SELECT Id,MenuName FROM [dbo].[NavigationMenuList] where RootId=0 order by DisplayOrder";
                    _connection.Open();

                    SqlDataReader dr = _command.ExecuteReader();
                    while (dr.Read())
                    {
                        NavigationMenuList menu = new NavigationMenuList
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            MenuName = dr["MenuName"].ToString()
                        };

                        _MainMenuLists.Add(menu);
                    }
                    _connection.Close();


                }
                catch (Exception ex)
                {
                    // Log or print the exception details
                    Console.WriteLine($"Exception: {ex.Message}");
                }
                return _MainMenuLists;

            }
        }


        public List<NavigationMenuList> getAllSubMenus()
        {
            List<NavigationMenuList> _SubMenuLists = new List<NavigationMenuList>();


            using (_connection = new SqlConnection(GetConnectionStringg()))
            {
                try
                {
                    _command = _connection.CreateCommand();

                    _command.CommandText = "SELECT id,RootId,MenuName FROM [dbo].[NavigationMenuList] where RootId !=0 order by RootId,DisplayOrder";
                    _connection.Open();

                    SqlDataReader dr = _command.ExecuteReader();
                    while (dr.Read())
                    {
                        NavigationMenuList Submenus = new NavigationMenuList
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            RootId = Convert.ToInt32(dr["RootId"]),
                            MenuName = dr["MenuName"].ToString()
                        };

                        _SubMenuLists.Add(Submenus);
                    }
                    _connection.Close();


                }
                catch (Exception ex)
                {
                    // Log or print the exception details
                    Console.WriteLine($"Exception: {ex.Message}");
                }
                return _SubMenuLists;

            }
        }



    }
}
