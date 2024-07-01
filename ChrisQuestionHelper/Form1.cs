using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ChrisQuestionHelper
{
    public partial class Form1 : Form
    {
        private List<Question> questions = new List<Question>();

        public Form1()
        {
            InitializeComponent();
            CustomizeUI();
            PopulateComboBoxes();
            LoadQuestions(); // Initial load of questions

            // Check if there are no questions loaded
            if (!questions.Any())
            {
                MessageBox.Show("There are currently no questions available. Please use the 'Edit Questions' button to add some!");
            }

            // Set background image and layout
            string resourceName = "ChrisQuestionHelper.Background.png";
            Stream backgroundImageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (backgroundImageStream != null)
            {
                this.BackgroundImageLayout = ImageLayout.Zoom; // Ensure image scales without distortion
                this.BackgroundImage = new Bitmap(backgroundImageStream);
            }
            else
            {
                MessageBox.Show("Background image not found: background.png");
            }

            // Set default selections for ComboBoxes
            YearLevel.SelectedIndex = 0; // Select "Year Level?" as default
            comboBoxNumQuestions.SelectedIndex = 0; // Select "Amount of questions?" as default

            // Make the form non-resizable
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void CustomizeUI()
        {
            // Set colors and styles for UI elements
            // Ensure these settings complement your background image
            BackColor = Color.White;
            Subject.BackColor = Color.FromArgb(240, 240, 240);
            Subject.DropDownStyle = ComboBoxStyle.DropDownList;
            Subject.Font = new Font("Segoe UI", 11);
            Subject.ForeColor = Color.Black;

            YearLevel.BackColor = Color.FromArgb(240, 240, 240);
            YearLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            YearLevel.Font = new Font("Segoe UI", 11);
            YearLevel.ForeColor = Color.Black;

            comboBoxNumQuestions.BackColor = Color.FromArgb(240, 240, 240);
            comboBoxNumQuestions.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNumQuestions.Font = new Font("Segoe UI", 11);
            comboBoxNumQuestions.ForeColor = Color.Black;

            button1.BackColor = Color.FromArgb(13, 94, 175); // #0d5eaf
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            Edit.BackColor = Color.FromArgb(0, 32, 96); // #002060
            Edit.FlatStyle = FlatStyle.Flat;
            Edit.ForeColor = Color.White;
            Edit.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            textBoxQuestions.BackColor = Color.FromArgb(240, 240, 240);
            textBoxQuestions.Font = new Font("Segoe UI", 11);
            textBoxQuestions.ForeColor = Color.Black;
            textBoxQuestions.BorderStyle = BorderStyle.FixedSingle;
            textBoxQuestions.Multiline = true;
            textBoxQuestions.ScrollBars = ScrollBars.Vertical; // Add vertical scrollbar
        }

        private void PopulateComboBoxes()
        {
            // Populate the year level ComboBox
            YearLevel.Items.Add("Year Level?");
            YearLevel.Items.AddRange(new string[] { "Year 7", "Year 8", "Year 9", "Year 10", "Year 11", "Year 12" });
            YearLevel.SelectedIndexChanged += YearLevel_SelectedIndexChanged;

            // Populate the number of questions ComboBox
            comboBoxNumQuestions.Items.Add("Amount of questions?");
            comboBoxNumQuestions.Items.AddRange(new string[] { "5", "10", "15" });

            // Attach event handler for Subject ComboBox
            Subject.SelectedIndexChanged += Subject_SelectedIndexChanged;
        }

        private void YearLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Subject.Items.Clear();
            string selectedYearLevel = YearLevel.SelectedItem.ToString();

            switch (selectedYearLevel)
            {
                case "Year Level?":
                    Subject.Items.Add("Subject?");
                    break;
                case "Year 7":
                case "Year 8":
                case "Year 9":
                case "Year 10":
                case "Year 11":
                case "Year 12":
                    Subject.Items.AddRange(new string[] {
                        "Number and Algebra",
                        "Measurement and Geometry",
                        "Statistics and Probability"
                    });
                    break;
                default:
                    Subject.Items.AddRange(new string[] {
                        "Geometry",
                        "Trigonometry",
                        "Calculus"
                    });
                    break;
            }

            Subject.SelectedIndex = 0; // Ensure default selection after adding items
        }

        private void Subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNumQuestions.SelectedItem?.ToString() == "Amount of questions?")
            {
                comboBoxNumQuestions.SelectedItem = "5"; // Default to 5 if "Amount of questions?" is selected
            }
        }

        private void LoadQuestions()
        {
            try
            {
                questions.Clear(); // Clear existing questions

                // Specify the full resource name
                string resourceName = "ChrisQuestionHelper.questions.txt";

                // Access the embedded resource stream
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        MessageBox.Show("Embedded resource 'questions.txt' not found.");
                        return;
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(new[] { "||" }, StringSplitOptions.None);
                            if (parts.Length == 3)
                            {
                                questions.Add(new Question
                                {
                                    Subject = parts[0],
                                    YearLevel = parts[1],
                                    Text = parts[2]
                                });
                            }
                        }
                    }
                }

                // Load questions from the external file as well
                string externalFilePath = "questions.txt";
                if (File.Exists(externalFilePath))
                {
                    var lines = File.ReadAllLines(externalFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(new[] { "||" }, StringSplitOptions.None);
                        if (parts.Length == 3)
                        {
                            questions.Add(new Question
                            {
                                Subject = parts[0],
                                YearLevel = parts[1],
                                Text = parts[2]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadQuestions(); // Reload questions from embedded resource

            if (!questions.Any())
            {
                MessageBox.Show("There are currently no questions available. Please use the 'Edit Questions' button to add some!");
                return;
            }

            string selectedSubject = Subject.SelectedItem?.ToString();
            string selectedYearLevel = YearLevel.SelectedItem?.ToString();
            int numQuestions;

            if (comboBoxNumQuestions.SelectedItem?.ToString() == "Amount of questions?")
            {
                numQuestions = 5; // Default to 5 if "Amount of questions?" is selected
            }
            else if (!int.TryParse(comboBoxNumQuestions.SelectedItem?.ToString(), out numQuestions))
            {
                MessageBox.Show("Please select a valid number of questions.");
                return;
            }

            if (string.IsNullOrEmpty(selectedSubject) || string.IsNullOrEmpty(selectedYearLevel))
            {
                MessageBox.Show("Please select both a subject and a year level.");
                return;
            }

            var filteredQuestions = questions
                .Where(q => q.Subject == selectedSubject && q.YearLevel == selectedYearLevel)
                .OrderBy(x => Guid.NewGuid()) // Shuffle the questions randomly
                .Take(numQuestions)            // Take the selected number of questions
                .ToList();

            textBoxQuestions.Clear();
            foreach (var question in filteredQuestions)
            {
                textBoxQuestions.AppendText(question.Text + Environment.NewLine);
            }
        }

        private void Edit_Click_1(object sender, EventArgs e)
        {
            EditQuestionsForm editForm = new EditQuestionsForm();
            editForm.ShowDialog(); // Open edit form as dialog

            // Reload questions after editing
            LoadQuestions(); // Ensure questions are reloaded from embedded resource
        }

        // Other methods and event handlers

        public class Question
        {
            public string Subject { get; set; }
            public string YearLevel { get; set; }
            public string Text { get; set; }
        }
    }
}
