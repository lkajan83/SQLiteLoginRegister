using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using System;
using SQLiteLoginRegister.Models;

namespace SQLiteLoginRegister
{
    [Activity(Label = "SQLiteLoginRegister", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnsign = FindViewById<Button>(Resource.Id.btnlogin);
            btncreate = FindViewById<Button>(Resource.Id.btnregister);
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpwd);
            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;
            CreateDB();
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }
        private void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    //if you want you can tost 
                    //Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    //i am going to call other activity
                    StartActivity(typeof(StoryboardCode));
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}