using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CyberChatbotUI
{
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
                    "thank you", new List<string>
                    {
                        "You're welcome! Is there anything else I can help you with?",
                        "Anytime! Feel free to ask more questions."
                    }
                },
                {
                    "help", new List<string>
                    {
                        "I'm here to help with cybersecurity questions! You can ask me about passwords, phishing, malware, firewalls, VPNs, backups, and more.",
                        "What specific cybersecurity topic would you like help with?"
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
                return "I can help you with various cybersecurity topics including passwords, phishing, malware, firewalls, VPNs, backups, and general security best practices. What would you like to know?";
            }

            // Default response if no keyword is found
            return "I specialize in cybersecurity topics. You can ask me about passwords, phishing, malware, firewalls, VPNs, backups, or any other security-related questions!";
        }

        public string GetRandomCyberFact()
        {
            int index = random.Next(0, randomCyberFacts.Count);
            return $"💡 Cyber Tip: {randomCyberFacts[index]}";
        }

        public bool IsGoodbyeMessage(string userInput)
        {
            string input = userInput.ToLower();
            return input.Contains("goodbye") || input.Contains("bye") ||
                   input.Contains("exit") || input.Contains("quit") ||
                   input.Contains("see you") || input.Contains("farewell");
        }

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

    public partial class MainWindow : Window
    {
        private readonly ChatbotServices chatbotServices;
        private readonly Random random;
        private readonly TypingEffect typingEffect;
        private readonly List<string> conversationHistory;

        public MainWindow()
        {
            InitializeComponent();
            chatbotServices = new ChatbotServices();
            random = new Random();
            typingEffect = new TypingEffect(25);
            conversationHistory = new List<string>();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Welcome message with typing effect
            var welcomeBlock = AppendMessage("Cyber Helper", "");
            await typingEffect.TypeTextAsync(welcomeBlock, "🛡️ Welcome to Cyber Helper! I'm here to help you with all your cybersecurity questions.");

            await Task.Delay(1500);

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
            await ShowBotResponseAsync(ProcessUserInput(userInput));
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