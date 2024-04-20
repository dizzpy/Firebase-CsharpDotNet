﻿using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace FirebaseCsharpDotNet
{
    public partial class HomeView : Form
    {
        private FirestoreDb db;

        public HomeView()
        {
            InitializeComponent();
            InitializeFirestore();
        }

        private void InitializeFirestore()
        {
            string projectId = "fir-csdotnet";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\User\\Desktop\\Studee\\FirebaseCsharpDotNet\\FirebaseCsharpDotNet\\Credentials\\firebaseCredentials.json");
            db = FirestoreDb.Create(projectId);
        }


        private async void AddData_Click(object sender, EventArgs e)
        {
            try
            {
                string textData = AddText.Text;


                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "name", textData },
                };
                DocumentReference taskDocument = db.Collection("Collection-Name").Document("Document-Name");
                await taskDocument.SetAsync(data);

                MessageBox.Show("Data Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding data: " + ex.Message);
            }
        }

        private async void LoadData_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentReference taskDocument = db.Collection("Collection-Name").Document("Document-Name");
                DocumentSnapshot snapshot = await taskDocument.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    string name = snapshot.GetValue<string>("name");
                    Console.WriteLine(name);
                    DisplayText.Text = name;
                    MessageBox.Show("Data Loaded Successfully");
                }
                else
                {
                    MessageBox.Show("Document does not exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}

