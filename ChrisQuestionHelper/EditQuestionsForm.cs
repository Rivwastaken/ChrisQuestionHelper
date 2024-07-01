using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ChrisQuestionHelper
{
    public partial class EditQuestionsForm : Form
    {
        private string resourceName = "ChrisQuestionHelper.questions.txt";
        private string resourceNamespace = "ChrisQuestionHelper";
        private string externalFilePath = "questions.txt"; // External file path to store modified content
        private List<Question> questions = new List<Question>(); // List to store all questions

        public EditQuestionsForm()
        {
            InitializeComponent();
            CustomizeUI();
            PopulateYearLevelComboBox();
            LoadQuestionsFromExternalFile(); // Load all questions from external file

            // Set background image and layout
            string backgroundResourceName = "ChrisQuestionHelper.Background.png";
            Stream backgroundImageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(backgroundResourceName);
            if (backgroundImageStream != null)
            {
                this.BackgroundImageLayout = ImageLayout.Stretch; // Ensure image scales to fill the form
                this.BackgroundImage = new Bitmap(backgroundImageStream);
            }
            else
            {
                MessageBox.Show("Background image not found: Background.png");
            }
        }

        private void CustomizeUI()
        {
            BackColor = Color.White;

            comboBoxYearLevel.BackColor = Color.FromArgb(240, 240, 240);
            comboBoxYearLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxYearLevel.Font = new Font("Segoe UI", 11);
            comboBoxYearLevel.ForeColor = Color.Black;

            comboBoxSubject.BackColor = Color.FromArgb(240, 240, 240);
            comboBoxSubject.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSubject.Font = new Font("Segoe UI", 11);
            comboBoxSubject.ForeColor = Color.Black;

            textBoxQuestion.BackColor = Color.FromArgb(240, 240, 240);
            textBoxQuestion.Font = new Font("Segoe UI", 11);
            textBoxQuestion.ForeColor = Color.Black;
            textBoxQuestion.BorderStyle = BorderStyle.FixedSingle;
            textBoxQuestion.Multiline = true;
            textBoxQuestion.ScrollBars = ScrollBars.Vertical; // Add vertical scrollbar

            buttonSave.BackColor = Color.FromArgb(0, 32, 96); // #002060
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.ForeColor = Color.White;
            buttonSave.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // Add instructions label
            Label instructionsLabel = new Label
            {
                Text = "Enter each question on a new line in the box below.",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(220, 100),
                Location = new Point(220, 100), // Adjusted location to be below the top image
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            Controls.Add(instructionsLabel);
        }

        private void PopulateYearLevelComboBox()
        {
            comboBoxYearLevel.Items.Add("Year Level?");
            comboBoxYearLevel.Items.AddRange(new string[] { "Year 7", "Year 8", "Year 9", "Year 10", "Year 11", "Year 12" });
            comboBoxYearLevel.SelectedIndex = 0;
            comboBoxYearLevel.SelectedIndexChanged += ComboBoxYearLevel_SelectedIndexChanged;

            comboBoxSubject.SelectedIndexChanged += ComboBoxSubject_SelectedIndexChanged; // Ensure the subject ComboBox event is attached
        }

        private void ComboBoxYearLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSubject.Items.Clear();
            string selectedYearLevel = comboBoxYearLevel.SelectedItem.ToString();

            switch (selectedYearLevel)
            {
                case "Year Level?":
                    comboBoxSubject.Items.Add("Subject?");
                    comboBoxSubject.SelectedIndex = 0;
                    break;
                case "Year 7":
                case "Year 8":
                case "Year 9":
                case "Year 10":
                case "Year 11":
                case "Year 12":
                    comboBoxSubject.Items.AddRange(new string[] {
                        "Number and Algebra",
                        "Measurement and Geometry",
                        "Statistics and Probability"
                    });
                    comboBoxSubject.SelectedIndex = 0;
                    break;
                default:
                    comboBoxSubject.Items.AddRange(new string[] {
                        "Geometry",
                        "Trigonometry",
                        "Calculus"
                    });
                    comboBoxSubject.SelectedIndex = 0;
                    break;
            }

            DisplayQuestions();
        }

        private void ComboBoxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayQuestions(); // Call DisplayQuestions when the subject changes
        }

        private void LoadQuestionsFromExternalFile()
        {
            try
            {
                questions.Clear();
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
                else
                {
                    MessageBox.Show($"External file '{externalFilePath}' not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading external file: {ex.Message}");
            }
        }

        private void DisplayQuestions()
        {
            string selectedYearLevel = comboBoxYearLevel.SelectedItem?.ToString();
            string selectedSubject = comboBoxSubject.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedYearLevel) || string.IsNullOrEmpty(selectedSubject) || selectedYearLevel == "Year Level?" || selectedSubject == "Subject?")
            {
                textBoxQuestion.Clear();
                return;
            }

            var filteredQuestions = questions
                .Where(q => q.Subject == selectedSubject && q.YearLevel == selectedYearLevel)
                .Select(q => q.Text)
                .ToList();

            textBoxQuestion.Clear();
            foreach (var question in filteredQuestions)
            {
                textBoxQuestion.AppendText(question + Environment.NewLine);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string selectedYearLevel = comboBoxYearLevel.SelectedItem?.ToString();
            string selectedSubject = comboBoxSubject.SelectedItem?.ToString();
            string newQuestion = textBoxQuestion.Text.Trim();

            if (string.IsNullOrEmpty(selectedYearLevel) || string.IsNullOrEmpty(selectedSubject) || string.IsNullOrEmpty(newQuestion) || selectedYearLevel == "Year Level?" || selectedSubject == "Subject?")
            {
                MessageBox.Show("Please select a year level, subject, and enter a question.");
                return;
            }

            try
            {
                // Remove existing questions that match the selected year level and subject
                questions.RemoveAll(q => q.Subject == selectedSubject && q.YearLevel == selectedYearLevel);

                // Add new questions from the text box
                var newQuestions = newQuestion.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(q => new Question
                                              {
                                                  Subject = selectedSubject,
                                                  YearLevel = selectedYearLevel,
                                                  Text = q
                                              });

                questions.AddRange(newQuestions);

                // Overwrite the external file with the updated list of questions
                using (StreamWriter writer = new StreamWriter(externalFilePath))
                {
                    foreach (var question in questions)
                    {
                        writer.WriteLine($"{question.Subject}||{question.YearLevel}||{question.Text}");
                    }
                }

                MessageBox.Show("Questions saved successfully.");

                // Reload questions and display updated list
                LoadQuestionsFromExternalFile();
                DisplayQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving questions: {ex.Message}");
            }
        }

        public class Question
        {
            public string Subject { get; set; }
            public string YearLevel { get; set; }
            public string Text { get; set; }
        }
    }
}
