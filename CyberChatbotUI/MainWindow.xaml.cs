using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using Microsoft.VisualBasic;
using POE;

namespace CyberChatbotUI
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Reminder { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CompletedTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? TextDecorations.Strikethrough : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ChatbotServices
    {
        private readonly Random random;
        public readonly Dictionary<string, List<string>> cyberChatbotResponses;
        public readonly List<string> randomCyberFacts;

        public ChatbotServices()
        {
            random = new Random();

            cyberChatbotResponses = new Dictionary<string, List<string>>()
            {
                // Cybersecurity facts and information
                {
                    "purpose", new List<string>
                    {
                        "My purpose is to help you tackle every cyber threat you face. What issues are you currently facing?"
                    }
                },
                {
                    "phishing", new List<string>
                    {
                        "Phishing is a type of cyber attack where attackers trick people into revealing sensitive information, such as passwords, credit card numbers, or personal details. They often do this by disguising themselves as trustworthy entities, like banks, social media platforms, or government agencies.",
                        "To protect against phishing:\n• Check sender email for misspellings or odd domains\n• Don't click unknown links\n• Avoid urgent requests - scammers create fake urgency\n• Look for poor grammar\n• Verify with the source directly\n• Don't download unknown attachments\n• Use 2FA for added security\n• Keep software updated"
                    }
                },
                {
                    "password", new List<string>
                    {
                        "Use strong passwords with at least 12 characters, including uppercase, lowercase, numbers, and symbols.",
                        "Never reuse passwords across multiple accounts. Use a password manager!",
                        "Enable two-factor authentication whenever possible for extra security."
                    }
                },
                {
                    "malware", new List<string>
                    {
                        "Keep your antivirus software updated and run regular scans.",
                        "Don't download software from untrusted sources.",
                        "Be cautious with email attachments and USB drives from unknown sources."
                    }
                },
                {
                    "wifi", new List<string>
                    {
                        "Avoid using public Wi-Fi for sensitive activities like banking.",
                        "Use a VPN when connecting to public networks.",
                        "Make sure your home Wi-Fi uses WPA3 or WPA2 security."
                    }
                },
                {
                    "social", new List<string>
                    {
                        "Be careful what personal information you share on social media.",
                        "Check your privacy settings regularly on all social platforms.",
                        "Think twice before posting location information or travel plans."
                    }
                },
                {
                    "ransomware", new List<string>
                    {
                        "Ransomware encrypts your files and demands payment to unlock them. Regularly back up your data to protect against such attacks.",
                        "Never pay the ransom! There's no guarantee you'll get your files back, and it encourages further attacks.",
                        "Keep your operating system and all software up to date to patch vulnerabilities that ransomware can exploit."
                    }
                },
                {
                    "firewall", new List<string>
                    {
                        "A firewall acts as a barrier between your network and potential threats from the internet.",
                        "Make sure your firewall is enabled and properly configured.",
                        "Regularly review and update your firewall rules."
                    }
                },
                {
                    "vpn", new List<string>
                    {
                        "A VPN (Virtual Private Network) encrypts your internet connection and hides your IP address.",
                        "Use a reputable VPN service when connecting to public Wi-Fi.",
                        "VPNs help protect your privacy and data from cybercriminals."
                    }
                },
                {
                    "backup", new List<string>
                    {
                        "Regular backups are your best defense against data loss from ransomware or hardware failure.",
                        "Follow the 3-2-1 backup rule: 3 copies of data, 2 different media types, 1 offsite backup.",
                        "Test your backups regularly to ensure they work when needed."
                    }
                },
                // Greetings
                {
                    "hello", new List<string>
                    {
                        "Hello! How can I help you with cybersecurity today?",
                        "Hi there! What cybersecurity questions do you have?"
                    }
                },
                {
                    "good morning", new List<string>
                    {
                        "Good morning! How can I help you stay safe today?",
                        "Morning! Ready to tackle some cybersecurity questions?"
                    }
                },
                {
                    "good afternoon", new List<string>
                    {
                        "Good afternoon! How can I assist you?",
                        "Hope your day is going well! What can I help you with?"
                    }
                },
                {
                    "good evening", new List<string>
                    {
                        "Good evening! Need any cybersecurity tips tonight?",
                        "Evening! How can I help you stay secure?"
                    }
                },
                {
                    "hi", new List<string>
                    {
                        "Hi! How are you doing today?",
                        "Hello! What can I help you with?"
                    }
                },
                {
                    "hey", new List<string>
                    {
                        "Hey there! How can I assist you?",
                        "Hi! What cybersecurity questions do you have?"
                    }
                },
                // Responses to user states
                {
                    "good", new List<string>
                    {
                        "That's great to hear! How can I assist you today?"
                    }
                },
                {
                    "bad", new List<string>
                    {
                        "I'm sorry to hear that. Is there anything cybersecurity-related I can help you with?"
                    }
                },
                {
                    "worried", new List<string>
                    {
                        "I'm here to help. Let me know what you're worried about, especially if it's cybersecurity-related."
                    }
                },
                {
                    "I'm excited", new List<string>
                    {
                        "Love that enthusiasm! 🎉 What's got you so excited today?"
                    }
                },
                {
                    "I don't understand", new List<string>
                    {
                        "No worries, I'm here to clear things up! 😊 Can you tell me a bit more?"
                    }
                },
                {
                    "thank you", new List<string>
                    {
                        "You're welcome! Is there anything else I can help you with?",
                        "Anytime! Feel free to ask more questions."
                    }
                },
                {
                    "This is annoying", new List<string>
                    {
                        "I feel you, that sounds frustrating. 😣 Let's sort this out—what's the issue?"
                    }
                },
                {
                    "help", new List<string>
                    {
                        "I'm here to help with cybersecurity questions! You can ask me about passwords, phishing, malware, firewalls, VPNs, backups, and more.",
                        "What specific cybersecurity topic would you like help with?"
                    }
                },
                {
                    "add task", new List<string>
                    {
                        "Let's add a new cybersecurity task. Please provide the task title."
                    }
                }
            };

            randomCyberFacts = new List<string>()
            {
                "Always use two-factor authentication (2FA) to add an extra layer of security to your accounts.",
                "Phishing attacks account for over 80% of reported security incidents worldwide.",
                "A strong password should be at least 12 characters long and include a mix of letters, numbers, and symbols.",
                "Public Wi-Fi networks are not secure. Avoid accessing sensitive information while connected to them.",
                "Keep your software and operating system updated to protect against the latest vulnerabilities.",
                "Over 90% of malware is delivered via email. Be cautious of unexpected attachments or links.",
                "Using the same password across multiple accounts increases your risk of being hacked.",
                "Back up your data regularly to protect against ransomware attacks.",
                "Cybercriminals often impersonate trusted organizations. Always verify the sender before sharing sensitive information.",
                "The average cost of a data breach is $4.45 million globally.",
                "95% of successful cyber attacks are due to human error.",
                "A new malware sample is created every 2.8 seconds worldwide."
            };
        }

        //Contains the chatbot response
        public string GetChatbotResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please enter a valid input.";

            string input = userInput.ToLower().Trim();

            // Check for keyword matches
            foreach (var kvp in cyberChatbotResponses)
            {
                if (input.Contains(kvp.Key.ToLower()))
                {
                    return kvp.Value[random.Next(kvp.Value.Count)];
                }
            }

            // Special responses for common variations
            if (input.Contains("how are you") || input.Contains("how r u"))
            {
                return "I'm doing well, thank you! How can I help you with cybersecurity today?";
            }

            if (input.Contains("what can you do") || input.Contains("capabilities"))
            {
                return "I can help you with various cybersecurity topics including passwords, phishing, malware, firewalls, VPNs, backups, and general security best practices. You can also add and manage cybersecurity tasks. What would you like to know?";
            }

            // Default response if no keyword is found
            return "I'm not sure I get what you're saying, but I specialize in cybersecurity topics. You can ask me about passwords, phishing, malware, firewalls, VPNs, backups, or add a new task with 'add task'! 😊";
        }

        //Contains cybersecurity facts that are displayed randomly
        public string GetRandomCyberFact()
        {
            int index = random.Next(0, randomCyberFacts.Count);
            return $"💡 Cyber Tip: {randomCyberFacts[index]}";
        }

        //Contains and displays goodbye messages
        public bool IsGoodbyeMessage(string userInput)
        {
            string input = userInput.ToLower();
            return input.Contains("goodbye") || input.Contains("bye") ||
                   input.Contains("exit") || input.Contains("quit") ||
                   input.Contains("see you") || input.Contains("farewell");
        }

        //Contains and displays goodbye messages
        public string GetGoodbyeMessage()
        {
            var goodbyes = new List<string>
            {
                "Thank you for using Cyber Helper! Stay safe online! 🛡️",
                "Goodbye! Remember to keep your systems updated and stay vigilant! 👋",
                "Take care! Don't forget to use strong passwords and 2FA! 🔐"
            };
            return goodbyes[random.Next(goodbyes.Count)];
        }
    }

    //Typing effect logic
    public class TypingEffect
    {
        private readonly int typingSpeed;
        private readonly DispatcherTimer timer;
        private string fullText;
        private int currentIndex;
        private TextBlock targetTextBlock;
        private TaskCompletionSource<bool> completionSource;

        public TypingEffect(int millisecondsPerCharacter = 50)
        {
            typingSpeed = millisecondsPerCharacter;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        public Task TypeTextAsync(TextBlock textBlock, string text)
        {
            if (textBlock == null || string.IsNullOrEmpty(text))
                return Task.CompletedTask;

            StopTyping();

            targetTextBlock = textBlock;
            fullText = text;
            currentIndex = 0;
            completionSource = new TaskCompletionSource<bool>();

            targetTextBlock.Text = "";
            timer.Interval = TimeSpan.FromMilliseconds(typingSpeed);
            timer.Start();

            return completionSource.Task;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < fullText.Length)
            {
                targetTextBlock.Text += fullText[currentIndex];
                currentIndex++;
            }
            else
            {
                StopTyping();
                completionSource?.SetResult(true);
            }
        }

        public void StopTyping()
        {
            timer.Stop();
            if (targetTextBlock != null && !string.IsNullOrEmpty(fullText))
            {
                targetTextBlock.Text = fullText;
            }
        }
    }

    //Quiz service
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, List<string> options, int correctAnswerIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }
    }

    public class QuizService
    {
        private readonly List<QuizQuestion> questions;
        private readonly Random random;

        public QuizService()
        {
            random = new Random();
            questions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What does 'phishing' refer to in cybersecurity?",
                    new List<string>
                    {
                        "A) Catching fish with digital nets",
                        "B) Fraudulent attempts to obtain sensitive information",
                        "C) A type of computer virus",
                        "D) Network fishing protocols"
                    },
                    1, // Index 1 = Option B
                    "Phishing refers to fraudulent attempts to obtain sensitive information by impersonating trustworthy entities. Attackers often use fake emails, websites, or messages to trick users into revealing passwords, credit card numbers, or other personal data."
                ),

                new QuizQuestion(
                    "What is the recommended minimum length for a strong password?",
                    new List<string>
                    {
                        "A) 6 characters",
                        "B) 8 characters",
                        "C) 12 characters",
                        "D) 16 characters"
                    },
                    2, // Index 2 = Option C
                    "Security experts recommend passwords be at least 12 characters long. Longer passwords are exponentially harder to crack through brute force attacks. The password should also include a mix of uppercase, lowercase, numbers, and special characters."
                ),

                new QuizQuestion(
                    "What does 'Two-Factor Authentication' (2FA) provide?",
                    new List<string>
                    {
                        "A) Double encryption of data",
                        "B) An additional layer of security beyond passwords",
                        "C) Two different password requirements",
                        "D) Backup authentication servers"
                    },
                    1, // Index 1 = Option B
                    "Two-Factor Authentication adds an extra layer of security by requiring two different authentication factors: something you know (password) and something you have (phone, token) or something you are (biometrics). This makes accounts much harder to compromise."
                ),

                new QuizQuestion(
                    "Which of these is considered malware?",
                    new List<string>
                    {
                        "A) Firewall",
                        "B) Antivirus software",
                        "C) Ransomware",
                        "D) VPN client"
                    },
                    2, // Index 2 = Option C
                    "Ransomware is a type of malware that encrypts a victim's files and demands payment for the decryption key. Other types of malware include viruses, worms, trojans, spyware, and adware. Firewalls, antivirus software, and VPNs are security tools, not malware."
                ),

                new QuizQuestion(
                    "What is the primary purpose of a firewall?",
                    new List<string>
                    {
                        "A) To speed up internet connections",
                        "B) To backup important files",
                        "C) To monitor and control network traffic",
                        "D) To clean computer viruses"
                    },
                    2, // Index 2 = Option C
                    "A firewall monitors and controls incoming and outgoing network traffic based on predetermined security rules. It acts as a barrier between trusted internal networks and untrusted external networks, helping prevent unauthorized access."
                ),

                new QuizQuestion(
                    "What should you do if you receive a suspicious email?",
                    new List<string>
                    {
                        "A) Forward it to all your contacts",
                        "B) Click the links to investigate",
                        "C) Reply asking for more information",
                        "D) Delete it without clicking any links"
                    },
                    3, // Index 3 = Option D
                    "When you receive a suspicious email, the safest action is to delete it without clicking any links or downloading attachments. If the email claims to be from a legitimate organization, contact them directly through official channels to verify its authenticity."
                ),

                new QuizQuestion(
                    "How often should you backup your important data?",
                    new List<string>
                    {
                        "A) Once a year",
                        "B) Once a month",
                        "C) Once a week",
                        "D) Daily or as frequently as data changes"
                    },
                    3, // Index 3 = Option D
                    "You should backup important data daily or as frequently as your data changes. The 3-2-1 backup rule is recommended: keep 3 copies of important data, store 2 backup copies on different storage media, and keep 1 backup copy offsite."
                ),

                new QuizQuestion(
                    "What does 'SSL/TLS' provide for web communications?",
                    new List<string>
                    {
                        "A) Faster website loading",
                        "B) Better search engine rankings",
                        "C) Encryption of data in transit",
                        "D) Automatic software updates"
                    },
                    2, // Index 2 = Option C
                    "SSL (Secure Sockets Layer) and TLS (Transport Layer Security) provide encryption for data transmitted between web browsers and servers. This ensures that sensitive information like passwords and credit card numbers cannot be intercepted and read by attackers."
                ),

                new QuizQuestion(
                    "Which practice makes you most vulnerable to cyber attacks?",
                    new List<string>
                    {
                        "A) Using the same password for multiple accounts",
                        "B) Installing security updates promptly",
                        "C) Using antivirus software",
                        "D) Enabling automatic backups"
                    },
                    0, // Index 0 = Option A
                    "Using the same password for multiple accounts is extremely dangerous. If one account is compromised, attackers can access all your other accounts using the same credentials. This is why security experts recommend using unique passwords for each account and a password manager to keep track of them."
                ),

                new QuizQuestion(
                    "What is 'social engineering' in cybersecurity?",
                    new List<string>
                    {
                        "A) Building secure social networks",
                        "B) Engineering social media platforms",
                        "C) Manipulating people to divulge confidential information",
                        "D) Creating user-friendly security interfaces"
                    },
                    2, // Index 2 = Option C
                    "Social engineering involves manipulating people psychologically to divulge confidential information or perform actions that compromise security. Attackers exploit human psychology rather than technical vulnerabilities, making people the weakest link in security."
                )
            };
        }

        //Retrieves quiz questions
        public List<QuizQuestion> GetRandomQuestions(int count = 5)
        {
            return questions.OrderBy(x => random.Next()).Take(count).ToList();
        }

        public List<QuizQuestion> GetAllQuestions()
        {
            return new List<QuizQuestion>(questions);
        }

        public QuizQuestion GetRandomQuestion()
        {
            return questions[random.Next(questions.Count)];
        }
    }

    //The main logic of the chatbot interface
    public partial class MainWindow : Window
    {
        private readonly VoiceGreeting voiceGreeting;
        private readonly ChatbotServices chatbotServices;
        private readonly Random random;
        private readonly TypingEffect typingEffect;
        private readonly List<string> conversationHistory;
        private readonly List<TaskItem> cyberTasks;
        private int quizScore = 0;

        private QuizService quizService;
        private List<QuizQuestion> currentQuiz;
        private int currentQuestionIndex = 0;
        private int totalQuizScore = 0;
        private int questionsAnswered = 0;
        private bool scanInProgress = false;
        private int totalQuizzesCompleted = 0;
        private int totalCorrectAnswers = 0;
        private int totalQuestionsAnswered = 0;
        private TaskItem pendingTask;

        public MainWindow()
        {
            voiceGreeting = new VoiceGreeting();
            InitializeComponent();
            chatbotServices = new ChatbotServices();
            random = new Random();
            typingEffect = new TypingEffect(25);
            conversationHistory = new List<string>();
            cyberTasks = new List<TaskItem>
            {
                new TaskItem { Title = "Update firewall rules", Description = "Review and update firewall configurations", IsCompleted = false },
                new TaskItem { Title = "Review security logs", Description = "Check system logs for suspicious activity", IsCompleted = false },
                new TaskItem { Title = "Patch management review", Description = "Ensure all systems are up to date", IsCompleted = false },
                new TaskItem { Title = "User access audit", Description = "Verify user permissions and access levels", IsCompleted = false },
                new TaskItem { Title = "Backup verification", Description = "Confirm backups are complete and accessible", IsCompleted = false }
            };

            Loaded += MainWindow_Loaded;
            TasksItemsControl.ItemsSource = cyberTasks;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Welcome message with typing effect
            var welcomeBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(welcomeBlock, "🛡️ Welcome to Cyber Helper! I'm here to help you with all your cybersecurity questions.");

            await Task.Delay(1500);

            //Plays voice greeting recording
            voiceGreeting.PlayVoiceGreeting();

            // Show a random cyber fact
            var factBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(factBlock, chatbotServices.GetRandomCyberFact());

            await Task.Delay(1000);

            // Prompt for questions
            var promptBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(promptBlock, "Feel free to ask me about passwords, phishing, malware, or any other cybersecurity topics! 💬");
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SidebarBorder.Tag?.ToString() == "Hidden")
            {
                SidebarBorder.Tag = "Visible";
                MenuToggleButton.Content = "☰";
            }
            else
            {
                SidebarBorder.Tag = "Hidden";
                MenuToggleButton.Content = "☰";
            }
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = ChatTab;
        }

        private void CybersecurityQuiz_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = QuizTab;
        }

        private void Cybertasks_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = TasksTab;
        }

        private void NetworkStatus_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = NetworkTab;
        }

        private void ThreatAnalysis_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = ThreatTab;
        }

        private void VulnerabilityScan_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = VulnerabilityTab;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = SettingsTab;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedItem = HelpTab;
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Initialize quiz if not already started
            if (currentQuiz == null)
            {
                StartNewQuiz();
                return;
            }

            // Find selected radio button in the quiz
            var quizPanel = FindName("QuizTab") as TabItem;
            if (quizPanel?.Content is StackPanel mainPanel)
            {
                var border = mainPanel.Children.OfType<Border>().FirstOrDefault();
                if (border?.Child is StackPanel borderPanel)
                {
                    var radioButtons = borderPanel.Children.OfType<RadioButton>().ToList();
                    var selectedButton = radioButtons.FirstOrDefault(rb => rb.IsChecked == true);

                    if (selectedButton != null)
                    {
                        var currentQuestion = currentQuiz[currentQuestionIndex];
                        int selectedIndex = radioButtons.IndexOf(selectedButton);

                        questionsAnswered++;

                        // Check if answer is correct
                        if (selectedIndex == currentQuestion.CorrectAnswerIndex)
                        {
                            MessageBox.Show($"Correct! 🎉\n\n{currentQuestion.Explanation}",
                                          "Quiz Result", MessageBoxButton.OK, MessageBoxImage.Information);
                            totalQuizScore++;
                        }
                        else
                        {
                            string correctOption = currentQuestion.Options[currentQuestion.CorrectAnswerIndex];
                            MessageBox.Show($"Incorrect. ❌\n\nThe correct answer is: {correctOption}\n\n{currentQuestion.Explanation}",
                                          "Quiz Result", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        // Move to next question or show final results
                        currentQuestionIndex++;

                        if (currentQuestionIndex >= currentQuiz.Count)
                        {
                            ShowQuizResults();
                        }
                        else
                        {
                            LoadCurrentQuestion();
                        }

                        // Reset radio buttons
                        foreach (var rb in radioButtons)
                            rb.IsChecked = false;
                    }
                    else
                    {
                        MessageBox.Show("Please select an answer before submitting.", "No Selection",
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void StartNewQuiz()
        {
            quizService = new QuizService();
            currentQuiz = quizService.GetRandomQuestions();
            currentQuestionIndex = 0;
            totalQuizScore = 0;
            questionsAnswered = 0;
            LoadCurrentQuestion();
        }

        private void LoadCurrentQuestion()
        {
            if (currentQuiz == null || currentQuestionIndex >= currentQuiz.Count) return;

            var question = currentQuiz[currentQuestionIndex];

            // Update the quiz UI
            var quizPanel = FindName("QuizTab") as TabItem;
            if (quizPanel?.Content is StackPanel mainPanel)
            {
                var border = mainPanel.Children.OfType<Border>().FirstOrDefault();
                if (border?.Child is StackPanel borderPanel)
                {
                    // Update question number and text
                    var questionNumberBlock = borderPanel.Children.OfType<TextBlock>().FirstOrDefault();
                    var questionTextBlock = borderPanel.Children.OfType<TextBlock>().Skip(1).FirstOrDefault();

                    if (questionNumberBlock != null)
                        questionNumberBlock.Text = $"Question {currentQuestionIndex + 1} of {currentQuiz.Count}:";

                    if (questionTextBlock != null)
                        questionTextBlock.Text = question.Question;

                    // Update radio button options
                    var radioButtons = borderPanel.Children.OfType<RadioButton>().ToList();
                    for (int i = 0; i < radioButtons.Count && i < question.Options.Count; i++)
                    {
                        radioButtons[i].Content = question.Options[i];
                        radioButtons[i].IsChecked = false;
                    }

                    // Update button text
                    var submitButton = borderPanel.Children.OfType<Button>().FirstOrDefault();
                    if (submitButton != null)
                    {
                        if (currentQuestionIndex == currentQuiz.Count - 1)
                            submitButton.Content = "Finish Quiz";
                        else
                            submitButton.Content = "Submit Answer";
                    }
                }
            }
        }

        private void LoadInitialQuizState()
        {
            var quizPanel = FindName("QuizTab") as TabItem;
            if (quizPanel?.Content is StackPanel mainPanel)
            {
                var border = mainPanel.Children.OfType<Border>().FirstOrDefault();
                if (border?.Child is StackPanel borderPanel)
                {
                    // Reset to initial question
                    var questionNumberBlock = borderPanel.Children.OfType<TextBlock>().FirstOrDefault();
                    var questionTextBlock = borderPanel.Children.OfType<TextBlock>().Skip(1).FirstOrDefault();

                    if (questionNumberBlock != null)
                        questionNumberBlock.Text = "Click 'Start Quiz' to begin:";

                    if (questionTextBlock != null)
                        questionTextBlock.Text = "Test your cybersecurity knowledge with our interactive quiz!";

                    // Reset radio buttons
                    var radioButtons = borderPanel.Children.OfType<RadioButton>().ToList();
                    var defaultOptions = new List<string>
                    {
                        "A) Ready to start!",
                        "B) Let's test my knowledge!",
                        "C) I'm prepared for the challenge!",
                        "D) Bring on the questions!"
                    };

                    for (int i = 0; i < radioButtons.Count && i < defaultOptions.Count; i++)
                    {
                        radioButtons[i].Content = defaultOptions[i];
                        radioButtons[i].IsChecked = false;
                    }

                    // Update button
                    var submitButton = borderPanel.Children.OfType<Button>().FirstOrDefault();
                    if (submitButton != null)
                        submitButton.Content = "Start Quiz";
                }
            }
        }

        private void PracticeMode_Click(object sender, RoutedEventArgs e)
        {
            var practiceQuestion = quizService.GetRandomQuestion();

            // Update UI with practice question
            var quizPanel = FindName("QuizTab") as TabItem;
            if (quizPanel?.Content is StackPanel mainPanel)
            {
                var border = mainPanel.Children.OfType<Border>().FirstOrDefault();
                if (border?.Child is StackPanel borderPanel)
                {
                    var questionNumberBlock = borderPanel.Children.OfType<TextBlock>().FirstOrDefault();
                    var questionTextBlock = borderPanel.Children.OfType<TextBlock>().Skip(1).FirstOrDefault();

                    if (questionNumberBlock != null)
                        questionNumberBlock.Text = "Practice Mode - Single Question:";

                    if (questionTextBlock != null)
                        questionTextBlock.Text = practiceQuestion.Question;

                    var radioButtons = borderPanel.Children.OfType<RadioButton>().ToList();
                    for (int i = 0; i < radioButtons.Count && i < practiceQuestion.Options.Count; i++)
                    {
                        radioButtons[i].Content = practiceQuestion.Options[i];
                        radioButtons[i].IsChecked = false;
                    }

                    var submitButton = borderPanel.Children.OfType<Button>().FirstOrDefault();
                    if (submitButton != null)
                        submitButton.Content = "Check Answer";
                }
            }

            // Set up practice mode
            currentQuiz = new List<QuizQuestion> { practiceQuestion };
            currentQuestionIndex = 0;
        }

        private void ViewStats_Click(object sender, RoutedEventArgs e)
        {
            double accuracy = totalQuestionsAnswered > 0 ?
                (double)totalCorrectAnswers / totalQuestionsAnswered * 100 : 0;

            string stats = $"📊 Your Quiz Statistics\n\n" +
                           $"Quizzes Completed: {totalQuizzesCompleted}\n" +
                           $"Questions Answered: {totalQuestionsAnswered}\n" +
                           $"Correct Answers: {totalCorrectAnswers}\n" +
                           $"Overall Accuracy: {accuracy:F1}%\n\n";

            if (accuracy >= 90)
                stats += "🏆 Excellent performance! You're a cybersecurity expert!";
            else if (accuracy >= 80)
                stats += "🥇 Great job! You have strong cybersecurity knowledge!";
            else if (accuracy >= 70)
                stats += "🥈 Good work! Keep practicing to improve further!";
            else if (accuracy >= 60)
                stats += "🥉 Fair performance. Consider reviewing cybersecurity basics.";
            else if (totalQuestionsAnswered > 0)
                stats += "📚 Keep studying! Practice makes perfect!";
            else
                stats += "🎯 Take some quizzes to see your statistics!";

            MessageBox.Show(stats, "Quiz Statistics", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowQuizResults()
        {
            double percentage = (double)totalQuizScore / currentQuiz.Count * 100;
            string grade;
            string message;

            // Update statistics
            totalQuizzesCompleted++;
            totalQuestionsAnswered += currentQuiz.Count;
            totalCorrectAnswers += totalQuizScore;

            if (percentage >= 90)
            {
                grade = "Excellent! 🏆";
                message = "You have outstanding cybersecurity knowledge!";
            }
            else if (percentage >= 80)
            {
                grade = "Very Good! 🥇";
                message = "You have strong cybersecurity awareness!";
            }
            else if (percentage >= 70)
            {
                grade = "Good! 🥈";
                message = "You have decent cybersecurity knowledge with room for improvement.";
            }
            else if (percentage >= 60)
            {
                grade = "Fair 🥉";
                message = "You should review cybersecurity best practices.";
            }
            else
            {
                grade = "Needs Improvement 📚";
                message = "Consider studying cybersecurity fundamentals more thoroughly.";
            }

            string results = $"Quiz Complete!\n\n" +
                            $"Score: {totalQuizScore}/{currentQuiz.Count} ({percentage:F1}%)\n" +
                            $"Grade: {grade}\n\n" +
                            $"{message}\n\n" +
                            $"Overall Accuracy: {(double)totalCorrectAnswers / totalQuestionsAnswered * 100:F1}%\n" +
                            $"Would you like to take another quiz?";

            var result = MessageBox.Show(results, "Quiz Results",
                                        MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                StartNewQuiz();
            }
            else
            {
                // Reset to initial state
                currentQuiz = null;
                LoadInitialQuizState();
            }
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            string newTaskTitle = Interaction.InputBox("Enter a new cybersecurity task:", "Add New Task", "");
            if (!string.IsNullOrWhiteSpace(newTaskTitle))
            {
                string description = Interaction.InputBox("Enter task description:", "Task Description", "");
                string reminderInput = Interaction.InputBox("Enter reminder timeframe (e.g., '7 days' or '2025-12-31') or leave blank:", "Set Reminder", "");
                DateTime? reminder = null;

                if (!string.IsNullOrWhiteSpace(reminderInput))
                {
                    if (DateTime.TryParse(reminderInput, out DateTime specificDate))
                    {
                        reminder = specificDate;
                    }
                    else if (reminderInput.ToLower().Contains("day"))
                    {
                        if (int.TryParse(reminderInput.Replace("days", "").Trim(), out int days))
                        {
                            reminder = DateTime.Now.AddDays(days);
                        }
                    }
                }

                var newTask = new TaskItem
                {
                    Title = newTaskTitle,
                    Description = string.IsNullOrWhiteSpace(description) ? $"Complete {newTaskTitle.ToLower()}" : description,
                    Reminder = reminder,
                    IsCompleted = false
                };

                cyberTasks.Add(newTask);
                TasksItemsControl.ItemsSource = null;
                TasksItemsControl.ItemsSource = cyberTasks;

                MessageBox.Show($"Task '{newTaskTitle}' added successfully!", "Task Added", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TaskItem task)
            {
                string newTitle = Interaction.InputBox("Edit task title:", "Edit Task", task.Title);
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    string newDescription = Interaction.InputBox("Edit task description:", "Edit Description", task.Description);
                    string reminderInput = Interaction.InputBox("Edit reminder timeframe (e.g., '7 days' or '2025-12-31') or leave blank:", "Edit Reminder", task.Reminder?.ToString("yyyy-MM-dd") ?? "");

                    DateTime? newReminder = null;
                    if (!string.IsNullOrWhiteSpace(reminderInput))
                    {
                        if (DateTime.TryParse(reminderInput, out DateTime specificDate))
                        {
                            newReminder = specificDate;
                        }
                        else if (reminderInput.ToLower().Contains("day"))
                        {
                            if (int.TryParse(reminderInput.Replace("days", "").Trim(), out int days))
                            {
                                newReminder = DateTime.Now.AddDays(days);
                            }
                        }
                    }

                    task.Title = newTitle;
                    task.Description = string.IsNullOrWhiteSpace(newDescription) ? task.Description : newDescription;
                    task.Reminder = newReminder;

                    TasksItemsControl.ItemsSource = null;
                    TasksItemsControl.ItemsSource = cyberTasks;

                    MessageBox.Show($"Task '{newTitle}' updated successfully!", "Task Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is TaskItem task)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the task '{task.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    cyberTasks.Remove(task);
                    TasksItemsControl.ItemsSource = null;
                    TasksItemsControl.ItemsSource = cyberTasks;
                    MessageBox.Show($"Task '{task.Title}' deleted successfully!", "Task Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void TaskCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsCompleted = true;
                TasksItemsControl.ItemsSource = null;
                TasksItemsControl.ItemsSource = cyberTasks;
            }
        }

        private void TaskCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsCompleted = false;
                TasksItemsControl.ItemsSource = null;
                TasksItemsControl.ItemsSource = cyberTasks;
            }
        }

        private async void StartNewScan_Click(object sender, RoutedEventArgs e)
        {
            if (scanInProgress)
            {
                MessageBox.Show("A scan is already in progress. Please wait for it to complete.",
                              "Scan In Progress", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            scanInProgress = true;
            var button = sender as Button;
            var originalContent = button.Content;
            button.Content = "Scanning...";
            button.IsEnabled = false;

            // Find the progress bar
            var vulnTab = FindName("VulnerabilityTab") as TabItem;
            if (vulnTab?.Content is StackPanel mainPanel)
            {
                var border = mainPanel.Children.OfType<Border>().FirstOrDefault();
                if (border?.Child is StackPanel borderPanel)
                {
                    var progressBar = borderPanel.Children.OfType<ProgressBar>().FirstOrDefault();
                    var progressText = borderPanel.Children.OfType<TextBlock>().Skip(1).FirstOrDefault();

                    if (progressBar != null && progressText != null)
                    {
                        // Simulate scan progress
                        for (int i = 0; i <= 100; i += 5)
                        {
                            progressBar.Value = i;
                            progressText.Text = $"Scan Progress: {i}%";
                            await Task.Delay(200);
                        }
                    }
                }
            }

            // Show scan results
            var vulnerabilities = new[]
            {
                "2 Critical vulnerabilities found",
                "5 High-risk issues detected",
                "12 Medium-risk items identified",
                "8 Low-risk warnings"
            };

            string results = "🔍 Vulnerability Scan Complete!\n\n" + string.Join("\n", vulnerabilities) +
                           "\n\nRecommendation: Address critical and high-risk vulnerabilities immediately.";

            MessageBox.Show(results, "Scan Results", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset button
            button.Content = originalContent;
            button.IsEnabled = true;
            scanInProgress = false;
        }

        private void RecentChatsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecentChatsListBox.SelectedItem is ListBoxItem selectedItem)
            {
                // Switch to chat tab and show relevant conversation
                MainTabControl.SelectedItem = ChatTab;

                // Add a message about the selected topic
                var topicMessage = $"Let's discuss {selectedItem.Content}. What would you like to know?";
                _ = ShowBotResponseAsync(topicMessage);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            _ = SendMessageAsync();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                e.Handled = true;
                _ = SendMessageAsync();
            }
        }

        private async Task SendMessageAsync()
        {
            string userInput = InputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput)) return;

            // Add user message
            AppendMessage("You", userInput);
            conversationHistory.Add($"User: {userInput}");
            InputTextBox.Clear();

            // Show thinking indicator
            var thinkingBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(thinkingBlock, "🤔 Thinking...");
            await Task.Delay(800);

            // Remove thinking indicator
            ChatStack.Children.Remove(thinkingBlock);

            // Process input and show response
            await ProcessChatInputAsync(userInput);
        }

        private async Task ProcessChatInputAsync(string userInput)
        {
            string input = userInput.ToLower().Trim();

            if (input.StartsWith("add task"))
            {
                string taskTitle = input.Replace("add task", "").Trim();
                if (!string.IsNullOrWhiteSpace(taskTitle))
                {
                    pendingTask = new TaskItem
                    {
                        Title = taskTitle,
                        Description = $"Complete {taskTitle.ToLower()}",
                        IsCompleted = false
                    };
                    await ShowBotResponseAsync($"Task '{taskTitle}' added with description: '{pendingTask.Description}'. Would you like to set a reminder? (e.g., '7 days' or '2025-12-31')");
                }
                else
                {
                    await ShowBotResponseAsync("Please provide a task title. For example, 'add task Review privacy settings'.");
                }
            }
            else if (pendingTask != null)
            {
                if (input.ToLower() == "no" || input.ToLower() == "none")
                {
                    cyberTasks.Add(pendingTask);
                    TasksItemsControl.ItemsSource = null;
                    TasksItemsControl.ItemsSource = cyberTasks;
                    await ShowBotResponseAsync($"Task '{pendingTask.Title}' added successfully without a reminder!");
                    pendingTask = null;
                }
                else if (DateTime.TryParse(input, out DateTime specificDate))
                {
                    pendingTask.Reminder = specificDate;
                    cyberTasks.Add(pendingTask);
                    TasksItemsControl.ItemsSource = null;
                    TasksItemsControl.ItemsSource = cyberTasks;
                    await ShowBotResponseAsync($"Task '{pendingTask.Title}' added with reminder set for {specificDate:yyyy-MM-dd}!");
                    pendingTask = null;
                }
                else if (input.ToLower().Contains("day"))
                {
                    if (int.TryParse(input.Replace("days", "").Trim(), out int days))
                    {
                        pendingTask.Reminder = DateTime.Now.AddDays(days);
                        cyberTasks.Add(pendingTask);
                        TasksItemsControl.ItemsSource = null;
                        TasksItemsControl.ItemsSource = cyberTasks;
                        await ShowBotResponseAsync($"Task '{pendingTask.Title}' added with reminder set for {days} days from now!");
                        pendingTask = null;
                    }
                    else
                    {
                        await ShowBotResponseAsync("Invalid timeframe. Please specify a number of days (e.g., '7 days') or a date (e.g., '2025-12-31').");
                    }
                }
                else
                {
                    await ShowBotResponseAsync("Invalid input. Please specify a timeframe like '7 days' or a date like '2025-12-31', or say 'no' for no reminder.");
                }
            }
            else
            {
                // Process regular chatbot response
                await ShowBotResponseAsync(ProcessUserInput(userInput));
            }
        }

        private async Task ShowBotResponseAsync(string response)
        {
            // Create response block and type the message
            var responseBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(responseBlock, response);

            conversationHistory.Add($"Bot: {response}");

            // Occasionally add a random cybersecurity fact (25% chance)
            if (random.Next(1, 101) <= 25)
            {
                await Task.Delay(2000);
                var factBlock = AppendMessage("Cyber Helper", "");
                await typingEffect.TypeTextAsync(factBlock, chatbotServices.GetRandomCyberFact());
            }
        }

        private string ProcessUserInput(string userInput)
        {
            // Check for goodbye messages
            if (chatbotServices.IsGoodbyeMessage(userInput))
            {
                return chatbotServices.GetGoodbyeMessage();
            }

            // Get regular chatbot response
            return chatbotServices.GetChatbotResponse(userInput);
        }

        private TextBlock AppendMessage(string sender, string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm");
            string displayName = sender == "You" ? "You" : "🤖 Cyber Helper";

            // Create the message with proper formatting
            string fullMessage = string.IsNullOrEmpty(message) ? "" : $"[{timestamp}] {displayName}: {message}";

            TextBlock textBlock = new TextBlock
            {
                Text = fullMessage,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 8, 0, 5),
                Padding = new Thickness(10, 8, 10, 8),
                FontSize = 14,
                FontFamily = new FontFamily("Segoe UI"),
                Background = sender == "You" ? new SolidColorBrush(Color.FromRgb(230, 240, 255)) : new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                Foreground = sender == "You" ? Brushes.DarkBlue : Brushes.DarkGreen
            };

            // Add some visual styling
            textBlock.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                BlurRadius = 3,
                Opacity = 0.3,
                ShadowDepth = 1
            };

            ChatStack.Children.Add(textBlock);
            ScrollToBottom();
            return textBlock;
        }

        private void ScrollToBottom()
        {
            if (ChatStack.Parent is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}
