using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;

namespace BeautySalonManagement
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; } 
        public override string ToString()
        {
            return $"ID: {Id,-3} | {Name,-25} | Категория: {Category,-15} | Цена: {Price,8:C} | Длительность: {DurationMinutes,3} мин";
        }
    }

    public class Master
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; } 
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public Dictionary<DateTime, List<TimeSlot>> Schedule { get; set; } = new Dictionary<DateTime, List<TimeSlot>>(); 
        public override string ToString()
        {
            return $"ID: {Id,-3} | {Name,-20} | Специальность: {Specialty,-15} | Телефон: {Phone,-12} | Зарплата: {Salary,10:C}";
        }
    }

    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public override string ToString()
        {
            return $"ID: {Id,-3} | {Name,-20} | Телефон: {Phone,-12} | Email: {Email,-25}";
        }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public Client Client { get; set; }
        public Master Master { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public decimal TotalCost { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; } = false;
        public override string ToString()
        {
            return $"Запись #{Id} | Дата: {Date:dd.MM.yyyy} {StartTime} | Клиент: {Client.Name} | Мастер: {Master.Name} | Сумма: {FinalAmount:C}";
        }
    }

    public class SalonManagementSystem
    {
        private List<Service> services;
        private List<Master> masters;
        private List<Client> clients;
        private List<Appointment> appointments;
        private int nextServiceId = 1;
        private int nextMasterId = 1;
        private int nextClientId = 1;
        private int nextAppointmentId = 1;

        public SalonManagementSystem()
        {
            services = new List<Service>();
            masters = new List<Master>();
            clients = new List<Client>();
            appointments = new List<Appointment>();
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            services.Add(new Service { Id = nextServiceId++, Name = "Стрижка мужская", Category = "Парикмахерские", Price = 1000m, DurationMinutes = 30 });
            services.Add(new Service { Id = nextServiceId++, Name = "Окрашивание волос", Category = "Парикмахерские", Price = 3000m, DurationMinutes = 90 });
            services.Add(new Service { Id = nextServiceId++, Name = "Маникюр", Category = "Косметические", Price = 1500m, DurationMinutes = 45 });
            services.Add(new Service { Id = nextServiceId++, Name = "Чистка лица", Category = "Косметические", Price = 2000m, DurationMinutes = 60 });

            var master1 = new Master { Id = nextMasterId++, Name = "Анна Иванова", Specialty = "Парикмахер", Phone = "+7-900-123-45-67", Salary = 50000m, HireDate = DateTime.Now };
            var master2 = new Master { Id = nextMasterId++, Name = "Елена Смирнова", Specialty = "Косметолог", Phone = "+7-900-234-56-78", Salary = 45000m, HireDate = DateTime.Now };

            DateTime today = DateTime.Today;
            master1.Schedule[today] = new List<TimeSlot> { new TimeSlot { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(18) } };
            master1.Schedule[today.AddDays(1)] = new List<TimeSlot> { new TimeSlot { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(18) } };
            master2.Schedule[today] = new List<TimeSlot> { new TimeSlot { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(19) } };
            master2.Schedule[today.AddDays(1)] = new List<TimeSlot> { new TimeSlot { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(19) } };

            masters.Add(master1);
            masters.Add(master2);

            clients.Add(new Client { Id = nextClientId++, Name = "Петр Сидоров", Phone = "+7-900-345-67-89", Email = "petr@example.com", RegistrationDate = DateTime.Now });
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine(" ИНФОРМАЦИОННАЯ СИСТЕМА УПРАВЛЕНИЯ САЛОНОМ КРАСОТЫ");
                Console.WriteLine();
                Console.WriteLine("1. Управление каталогом услуг");
                Console.WriteLine("2. Управление мастерами и расписанием");
                Console.WriteLine("3. Управление клиентской базой");
                Console.WriteLine("4. Управление записями клиентов");
                Console.WriteLine("5. Управление кассой и платежами");
                Console.WriteLine("6. Отчёты и статистика");
                Console.WriteLine("7. Выход");
                Console.WriteLine();
                Console.Write("Выберите пункт меню (1-7): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ServiceManagement();
                        break;
                    case "2":
                        MasterManagement();
                        break;
                    case "3":
                        ClientManagement();
                        break;
                    case "4":
                        AppointmentManagement();
                        break;
                    case "5":
                        PaymentManagement();
                        break;
                    case "6":
                        Reports();
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ServiceManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine(" УПРАВЛЕНИЕ КАТАЛОГОМ УСЛУГ");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть все услуги");
                Console.WriteLine("2. Добавить новую услугу");
                Console.WriteLine("3. Обновить услугу");
                Console.WriteLine("4. Удалить услугу");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие (1-5): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ViewAllServices();
                        break;
                    case "2":
                        AddService();
                        break;
                    case "3":
                        UpdateService();
                        break;
                    case "4":
                        DeleteService();
                        break;
                    case "5":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewAllServices()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ УСЛУГИ");
            Console.WriteLine();
            if (services.Count == 0)
            {
                Console.WriteLine("Услуг нет");
            }
            else
            {
                var grouped = services.GroupBy(s => s.Category);
                foreach (var group in grouped)
                {
                    Console.WriteLine($"\n📁 Категория: {group.Key}");
                    Console.WriteLine(new string('-', 87));
                    foreach (var service in group)
                    {
                        Console.WriteLine(service);
                    }
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения");
            Console.ReadLine();
        }

        private void AddService()
        {
            Console.WriteLine(" ДОБАВИТЬ УСЛУГУ");
            Console.WriteLine();
            Console.Write("Введите название услуги: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите категорию (Парикмахерские/Косметические): ");
            string category = Console.ReadLine();
            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("Неверная цена.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите длительность в минутах: ");
            if (!int.TryParse(Console.ReadLine(), out int duration) || duration <= 0)
            {
                Console.WriteLine("Неверная длительность.");
                Console.ReadLine();
                return;
            }
            services.Add(new Service
            {
                Id = nextServiceId++,
                Name = name,
                Category = category,
                Price = price,
                DurationMinutes = duration
            });
            Console.WriteLine($"\n✓ Услуга '{name}' успешно добавлена.");
            Console.ReadLine();
        }

        private void UpdateService()
        {
            Console.WriteLine(" ОБНОВИТЬ УСЛУГУ");
            Console.WriteLine();
            Console.Write("Введите ID услуги для обновления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var service = services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                Console.WriteLine("Услуга не найдена.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nТекущие данные: {service}");
            Console.WriteLine();
            Console.Write("Введите новое название (Enter для пропуска): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                service.Name = name;
            Console.Write("Введите новую цену (Enter для пропуска): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
                service.Price = price;
            Console.Write("Введите новую длительность (Enter для пропуска): ");
            string durInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(durInput) && int.TryParse(durInput, out int duration))
                service.DurationMinutes = duration;
            Console.WriteLine($"\n✓ Услуга успешно обновлена.");
            Console.ReadLine();
        }

        private void DeleteService()
        {
            Console.WriteLine(" УДАЛИТЬ УСЛУГУ");
            Console.WriteLine();
            Console.Write("Введите ID услуги для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var service = services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                Console.WriteLine("Услуга не найдена.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nВы уверены, что хотите удалить '{service.Name}'? (д/н): ");
            if (Console.ReadLine().ToLower() == "д")
            {
                services.Remove(service);
                Console.WriteLine("✓ Услуга удалена.");
            }
            else
            {
                Console.WriteLine("✗ Удаление отменено.");
            }
            Console.ReadLine();
        }

        private void MasterManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine(" УПРАВЛЕНИЕ МАСТЕРАМИ И РАСПИСАНИЕМ");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть всех мастеров");
                Console.WriteLine("2. Добавить мастера");
                Console.WriteLine("3. Обновить данные мастера");
                Console.WriteLine("4. Удалить мастера");
                Console.WriteLine("5. Управление расписанием мастера");
                Console.WriteLine("6. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие (1-6): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ViewAllMasters();
                        break;
                    case "2":
                        AddMaster();
                        break;
                    case "3":
                        UpdateMaster();
                        break;
                    case "4":
                        DeleteMaster();
                        break;
                    case "5":
                        ScheduleManagement();
                        break;
                    case "6":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewAllMasters()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ МАСТЕРА");
            Console.WriteLine();
            if (masters.Count == 0)
            {
                Console.WriteLine("Мастеров нет.");
            }
            else
            {
                foreach (var master in masters)
                {
                    Console.WriteLine(master);
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void AddMaster()
        {
            Console.WriteLine(" ДОБАВИТЬ МАСТЕРА");
            Console.WriteLine();
            Console.Write("Введите ФИО: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ФИО не может быть пустым.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите специальность: ");
            string specialty = Console.ReadLine();
            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Введите зарплату: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary) || salary < 0)
            {
                Console.WriteLine("Неверная зарплата.");
                Console.ReadLine();
                return;
            }
            var master = new Master
            {
                Id = nextMasterId++,
                Name = name,
                Specialty = specialty,
                Phone = phone,
                Salary = salary,
                HireDate = DateTime.Now
            };
            masters.Add(master);
            Console.WriteLine($"\n✓ Мастер '{name}' успешно добавлен.");
            Console.ReadLine();
        }

        private void UpdateMaster()
        {
            Console.WriteLine(" ОБНОВИТЬ ДАННЫЕ МАСТЕРА");
            Console.WriteLine();
            Console.Write("Введите ID мастера: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var master = masters.FirstOrDefault(m => m.Id == id);
            if (master == null)
            {
                Console.WriteLine("Мастер не найден.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nТекущие данные: {master}");
            Console.WriteLine();
            Console.Write("Введите новое ФИО (Enter для пропуска): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                master.Name = name;
            Console.Write("Введите новую специальность (Enter для пропуска): ");
            string specialty = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(specialty))
                master.Specialty = specialty;
            Console.Write("Введите новый телефон (Enter для пропуска): ");
            string phone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(phone))
                master.Phone = phone;
            Console.Write("Введите новую зарплату (Enter для пропуска): ");
            string salaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(salaryInput) && decimal.TryParse(salaryInput, out decimal salary))
                master.Salary = salary;
            Console.WriteLine($"\n✓ Данные мастера успешно обновлены.");
            Console.ReadLine();
        }

        private void DeleteMaster()
        {
            Console.WriteLine(" УДАЛИТЬ МАСТЕРА");
            Console.WriteLine();
            Console.Write("Введите ID мастера: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var master = masters.FirstOrDefault(m => m.Id == id);
            if (master == null)
            {
                Console.WriteLine("Мастер не найден.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nВы уверены, что хотите удалить '{master.Name}'? (д/н): ");
            if (Console.ReadLine().ToLower() == "д")
            {
                masters.Remove(master);
                Console.WriteLine("✓ Мастер удалён.");
            }
            else
            {
                Console.WriteLine("✗ Удаление отменено.");
            }
            Console.ReadLine();
        }

        private void ScheduleManagement()
        {
            Console.WriteLine("Введите ID мастера: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var master = masters.FirstOrDefault(m => m.Id == id);
            if (master == null)
            {
                Console.WriteLine("Мастер не найден.");
                Console.ReadLine();
                return;
            }
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine($"РАСПИСАНИЕ ДЛЯ {master.Name}");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть расписание");
                Console.WriteLine("2. Добавить слот в расписание");
                Console.WriteLine("3. Вернуться");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewSchedule(master);
                        break;
                    case "2":
                        AddScheduleSlot(master);
                        break;
                    case "3":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewSchedule(Master master)
        {
            Console.Clear();
            Console.WriteLine($"РАСПИСАНИЕ {master.Name}");
            if (master.Schedule.Count == 0)
            {
                Console.WriteLine("Расписание пусто.");
            }
            else
            {
                foreach (var day in master.Schedule.OrderBy(s => s.Key))
                {
                    Console.WriteLine($"\nДата: {day.Key:dd.MM.yyyy}");
                    foreach (var slot in day.Value)
                    {
                        string status = slot.IsAvailable ? "Доступно" : "Занято";
                        Console.WriteLine($" {slot.StartTime} - {slot.EndTime} ({status})");
                    }
                }
            }
            Console.ReadLine();
        }

        private void AddScheduleSlot(Master master)
        {
            Console.Write("Введите дату (dd.MM.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Неверная дата.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите время начала (HH:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan start))
            {
                Console.WriteLine("Неверное время.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите время окончания (HH:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan end) || end <= start)
            {
                Console.WriteLine("Неверное время.");
                Console.ReadLine();
                return;
            }
            if (!master.Schedule.ContainsKey(date))
                master.Schedule[date] = new List<TimeSlot>();
            master.Schedule[date].Add(new TimeSlot { StartTime = start, EndTime = end });
            Console.WriteLine("✓ Слот добавлен.");
            Console.ReadLine();
        }

        private void ClientManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine(" УПРАВЛЕНИЕ КЛИЕНТСКОЙ БАЗОЙ");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть всех клиентов");
                Console.WriteLine("2. Добавить клиента");
                Console.WriteLine("3. Обновить данные клиента");
                Console.WriteLine("4. Удалить клиента");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие (1-5): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ViewAllClients();
                        break;
                    case "2":
                        AddClient();
                        break;
                    case "3":
                        UpdateClient();
                        break;
                    case "4":
                        DeleteClient();
                        break;
                    case "5":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewAllClients()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ КЛИЕНТЫ");
            Console.WriteLine();
            if (clients.Count == 0)
            {
                Console.WriteLine("Клиентов нет.");
            }
            else
            {
                foreach (var client in clients)
                {
                    Console.WriteLine(client);
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void AddClient()
        {
            Console.WriteLine(" ДОБАВИТЬ КЛИЕНТА");
            Console.WriteLine();
            Console.Write("Введите ФИО: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ФИО не может быть пустым.");
                Console.ReadLine();
                return;
            }
            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Введите email: ");
            string email = Console.ReadLine();
            clients.Add(new Client
            {
                Id = nextClientId++,
                Name = name,
                Phone = phone,
                Email = email,
                RegistrationDate = DateTime.Now
            });
            Console.WriteLine($"\n✓ Клиент '{name}' успешно добавлен.");
            Console.ReadLine();
        }

        private void UpdateClient()
        {
            Console.WriteLine(" ОБНОВИТЬ ДАННЫЕ КЛИЕНТА");
            Console.WriteLine();
            Console.Write("Введите ID клиента: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                Console.WriteLine("Клиент не найден.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nТекущие данные: {client}");
            Console.WriteLine();
            Console.Write("Введите новое ФИО (Enter для пропуска): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                client.Name = name;
            Console.Write("Введите новый телефон (Enter для пропуска): ");
            string phone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(phone))
                client.Phone = phone;
            Console.Write("Введите новый email (Enter для пропуска): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
                client.Email = email;
            Console.WriteLine($"\n✓ Данные клиента успешно обновлены.");
            Console.ReadLine();
        }

        private void DeleteClient()
        {
            Console.WriteLine(" УДАЛИТЬ КЛИЕНТА");
            Console.WriteLine();
            Console.Write("Введите ID клиента: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                Console.WriteLine("Клиент не найден.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nВы уверены, что хотите удалить '{client.Name}'? (д/н): ");
            if (Console.ReadLine().ToLower() == "д")
            {
                clients.Remove(client);
                Console.WriteLine("✓ Клиент удалён.");
            }
            else
            {
                Console.WriteLine("✗ Удаление отменено.");
            }
            Console.ReadLine();
        }

        // Управление записями
        private void AppointmentManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine(" УПРАВЛЕНИЕ ЗАПИСЯМИ КЛИЕНТОВ");
                Console.WriteLine();
                Console.WriteLine("1. Создать новую запись");
                Console.WriteLine("2. Просмотреть все записи");
                Console.WriteLine("3. Просмотреть детали записи");
                Console.WriteLine("4. Отменить запись");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие (1-5): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        CreateAppointment();
                        break;
                    case "2":
                        ViewAllAppointments();
                        break;
                    case "3":
                        ViewAppointmentDetails();
                        break;
                    case "4":
                        CancelAppointment();
                        break;
                    case "5":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void CreateAppointment()
        {
            Console.Clear();
            Console.WriteLine(" СОЗДАТЬ НОВУЮ ЗАПИСЬ");
            Console.WriteLine();

            ViewAllClients();
            Console.Write("Введите ID клиента: ");
            if (!int.TryParse(Console.ReadLine(), out int clientId))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var client = clients.FirstOrDefault(c => c.Id == clientId);
            if (client == null)
            {
                Console.WriteLine("Клиент не найден.");
                Console.ReadLine();
                return;
            }

            ViewAllMasters();
            Console.Write("Введите ID мастера: ");
            if (!int.TryParse(Console.ReadLine(), out int masterId))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var master = masters.FirstOrDefault(m => m.Id == masterId);
            if (master == null)
            {
                Console.WriteLine("Мастер не найден.");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите дату записи (dd.MM.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Неверная дата.");
                Console.ReadLine();
                return;
            }

            if (!master.Schedule.ContainsKey(date) || master.Schedule[date].All(s => !s.IsAvailable))
            {
                Console.WriteLine("Нет доступных слотов на эту дату.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Доступные слоты:");
            var availableSlots = master.Schedule[date].Where(s => s.IsAvailable).ToList();
            for (int i = 0; i < availableSlots.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableSlots[i].StartTime} - {availableSlots[i].EndTime}");
            }
            Console.Write("Выберите слот (номер): ");
            if (!int.TryParse(Console.ReadLine(), out int slotNum) || slotNum < 1 || slotNum > availableSlots.Count)
            {
                Console.WriteLine("Неверный выбор.");
                Console.ReadLine();
                return;
            }
            var selectedSlot = availableSlots[slotNum - 1];
            selectedSlot.IsAvailable = false;

            var appointment = new Appointment { Id = nextAppointmentId++, Date = date, StartTime = selectedSlot.StartTime, Client = client, Master = master };
            bool addingServices = true;
            while (addingServices)
            {
                ViewAllServices();
                Console.Write("Введите ID услуги (0 для завершения): ");
                if (!int.TryParse(Console.ReadLine(), out int serviceId) || serviceId == 0)
                {
                    if (appointment.Services.Count == 0)
                    {
                        Console.WriteLine("Запись не может быть без услуг.");
                        Console.ReadLine();
                        selectedSlot.IsAvailable = true; 
                        return;
                    }
                    addingServices = false;
                    break;
                }
                var service = services.FirstOrDefault(s => s.Id == serviceId);
                if (service == null)
                {
                    Console.WriteLine("Услуга не найдена.");
                    Console.ReadLine();
                    continue;
                }
                appointment.Services.Add(service);
                Console.WriteLine($"✓ {service.Name} добавлена.");
                Console.ReadLine();
            }

            appointment.TotalCost = appointment.Services.Sum(s => s.Price);
            Console.Write("Введите скидку (%): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal discountPercent) && discountPercent >= 0 && discountPercent <= 100)
            {
                appointment.Discount = appointment.TotalCost * (discountPercent / 100);
            }
            appointment.FinalAmount = appointment.TotalCost - appointment.Discount;

            appointments.Add(appointment);
            Console.WriteLine($"\n✓ Запись #{appointment.Id} создана успешно! Сумма: {appointment.FinalAmount:C}");
            Console.ReadLine();
        }

        private void ViewAllAppointments()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ ЗАПИСИ");
            Console.WriteLine();
            if (appointments.Count == 0)
            {
                Console.WriteLine("Записей нет.");
            }
            else
            {
                foreach (var app in appointments)
                {
                    Console.WriteLine(app);
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения");
            Console.ReadLine();
        }

        private void ViewAppointmentDetails()
        {
            Console.Write("Введите ID записи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var app = appointments.FirstOrDefault(a => a.Id == id);
            if (app == null)
            {
                Console.WriteLine("Запись не найдена.");
                Console.ReadLine();
                return;
            }
            Console.Clear();
            Console.WriteLine($" ДЕТАЛИ ЗАПИСИ #{app.Id}");
            Console.WriteLine();
            Console.WriteLine($"Дата: {app.Date:dd.MM.yyyy} {app.StartTime}");
            Console.WriteLine($"Клиент: {app.Client.Name}");
            Console.WriteLine($"Мастер: {app.Master.Name}");
            Console.WriteLine($"Скидка: {app.Discount:C}");
            Console.WriteLine($"Итоговая сумма: {app.FinalAmount:C}");
            Console.WriteLine($"Оплачено: {(app.IsPaid ? "Да" : "Нет")}");
            Console.WriteLine();
            Console.WriteLine("Услуги:");
            foreach (var service in app.Services)
            {
                Console.WriteLine($" {service.Name,-25} {service.Price,8:C}");
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void CancelAppointment()
        {
            Console.Write("Введите ID записи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var app = appointments.FirstOrDefault(a => a.Id == id);
            if (app == null)
            {
                Console.WriteLine("Запись не найдена.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nВы уверены, что хотите отменить запись #{app.Id}? (д/н): ");
            if (Console.ReadLine().ToLower() == "д")
            {
                if (app.Master.Schedule.ContainsKey(app.Date))
                {
                    var slot = app.Master.Schedule[app.Date].FirstOrDefault(s => s.StartTime == app.StartTime);
                    if (slot != null)
                        slot.IsAvailable = true;
                }
                appointments.Remove(app);
                Console.WriteLine("✓ Запись отменена.");
            }
            else
            {
                Console.WriteLine("✗ Отмена отменена.");
            }
            Console.ReadLine();
        }

        private void PaymentManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine(" УПРАВЛЕНИЕ КАССОЙ И ПЛАТЕЖАМИ");
                Console.WriteLine();
                Console.WriteLine("1. Оплатить запись");
                Console.WriteLine("2. Просмотреть неоплаченные записи");
                Console.WriteLine("3. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие (1-3): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        PayAppointment();
                        break;
                    case "2":
                        ViewUnpaidAppointments();
                        break;
                    case "3":
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void PayAppointment()
        {
            Console.Write("Введите ID записи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                Console.ReadLine();
                return;
            }
            var app = appointments.FirstOrDefault(a => a.Id == id);
            if (app == null)
            {
                Console.WriteLine("Запись не найдена.");
                Console.ReadLine();
                return;
            }
            if (app.IsPaid)
            {
                Console.WriteLine("Запись уже оплачена.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("\nДоступные методы оплаты:");
            Console.WriteLine("1. Наличные");
            Console.WriteLine("2. Карта");
            Console.WriteLine("3. Электронный кошелек");
            Console.Write("Выберите метод (1-3): ");
            app.PaymentMethod = Console.ReadLine() switch
            {
                "1" => "Наличные",
                "2" => "Карта",
                "3" => "Электронный кошелек",
                _ => "Наличные"
            };
            app.IsPaid = true;
            Console.WriteLine($"\n✓ Запись #{app.Id} оплачена: {app.FinalAmount:C}");
            Console.ReadLine();
        }

        private void ViewUnpaidAppointments()
        {
            Console.Clear();
            Console.WriteLine(" НЕОПЛАЧЕННЫЕ ЗАПИСИ");
            Console.WriteLine();
            var unpaid = appointments.Where(a => !a.IsPaid).ToList();
            if (unpaid.Count == 0)
            {
                Console.WriteLine("Все записи оплачены.");
            }
            else
            {
                foreach (var app in unpaid)
                {
                    Console.WriteLine(app);
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void Reports()
        {
            bool viewing = true;
            while (viewing)
            {
                Console.Clear();
                Console.WriteLine(" ОТЧЁТЫ И СТАТИСТИКА");
                Console.WriteLine();
                Console.WriteLine("1. Отчёт по услугам");
                Console.WriteLine("2. Отчёт по доходам мастеров");
                Console.WriteLine("3. Общий финансовый отчёт");
                Console.WriteLine("4. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите отчёт (1-4): ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ServicesReport();
                        break;
                    case "2":
                        MastersIncomeReport();
                        break;
                    case "3":
                        FinancialReport();
                        break;
                    case "4":
                        viewing = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ServicesReport()
        {
            Console.Clear();
            Console.WriteLine(" ОТЧЁТ ПО УСЛУГАМ");
            Console.WriteLine();
            if (appointments.Count == 0)
            {
                Console.WriteLine("Записей нет.");
            }
            else
            {
                var serviceUsage = new Dictionary<string, (int count, decimal amount)>();
                foreach (var app in appointments.Where(a => a.IsPaid))
                {
                    foreach (var service in app.Services)
                    {
                        if (serviceUsage.ContainsKey(service.Name))
                        {
                            var existing = serviceUsage[service.Name];
                            serviceUsage[service.Name] = (existing.count + 1, existing.amount + service.Price);
                        }
                        else
                        {
                            serviceUsage[service.Name] = (1, service.Price);
                        }
                    }
                }
                Console.WriteLine("Популярные услуги:");
                foreach (var svc in serviceUsage.OrderByDescending(s => s.Value.count))
                {
                    Console.WriteLine($" {svc.Key,-25} Оказано: {svc.Value.count,3} | Доход: {svc.Value.amount,10:C}");
                }
            }
            Console.WriteLine("\nНажмите Enter для продолжения");
            Console.ReadLine();
        }

        private void MastersIncomeReport()
        {
            Console.Clear();
            Console.WriteLine(" ОТЧЁТ ПО ДОХОДАМ МАСТЕРОВ");
            Console.WriteLine();
            var masterIncome = masters.ToDictionary(m => m.Name, m => 0m);
            foreach (var app in appointments.Where(a => a.IsPaid))
            {
                masterIncome[app.Master.Name] += app.FinalAmount;
            }
            foreach (var inc in masterIncome.OrderByDescending(i => i.Value))
            {
                Console.WriteLine($" {inc.Key,-20} Доход: {inc.Value,10:C}");
            }
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void FinancialReport()
        {
            Console.Clear();
            Console.WriteLine(" ФИНАНСОВЫЙ ОТЧЁТ");
            Console.WriteLine();
            decimal totalRevenue = appointments.Where(a => a.IsPaid).Sum(a => a.FinalAmount);
            decimal totalSalaries = masters.Sum(m => m.Salary);
            Console.WriteLine($"Общая выручка: {totalRevenue:C}");
            Console.WriteLine($"Затраты на зарплату: {totalSalaries:C}");
            Console.WriteLine($"Потенциальная прибыль: {(totalRevenue - totalSalaries):C}");
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var system = new SalonManagementSystem();
            system.Run();
        }
    }
}