# Peyghom

A modern messaging platform with chat, video calling, and AI-powered features.


## 📱 Overview

Peyghom is a full-featured messaging application that enables seamless communication between users. Beyond standard text messaging, Peyghom offers video calling capabilities and innovative AI features to enhance the user experience.

### Key Features

- **Real-time Messaging**: Send and receive messages instantly
- **Video Calls**: Connect face-to-face with crystal clear video calling
- **AI-Powered Features**: Smart replies, content suggestions, and more
- **User-Friendly Interface**: Intuitive design focused on user experience
- **Secure Communication**: End-to-end encryption for your privacy

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET Core
- **Architecture**: Modular Monolith
- **Database**: MongoDB, PostgreSQL
- **Authentication**: - 
- **Real-time Communication**: SignalR

### Frontend
- **Framework**: -
- **UI Library**: -
- **State Management**: -

## 🚀 Getting Started

### Prerequisites
- .NET SDK 8.0 or later
- Docker

### Installation

1. Clone the repository
```bash
git clone git@github.com:Hadi-bakhshi/Peyghom.git
cd Peyghom
```

2. Install dependencies
```bash
dotnet restore
```

3. Configure your database connection in `appsettings.json`

4. Run the application
```bash
dotnet run
```

5. Access the application at `http://localhost:5000`

## 📂 Project Structure

```
Peyghom/
├── src/
|   ├── API/              # API
│     ├── Peyghom.Api/              # API entry point
├── tests/
│   ├── Peyghom.UnitTests/
│   └── Peyghom.IntegrationTests/
└── docs/
    └── architecture/             # Architecture documentation
```

## 🔄 Development Workflow

1. Create a new branch for your feature/bugfix
2. Implement your changes
3. Write tests
4. Submit a pull request

## 📝 API Documentation

API documentation is available at `/openapi/peyghom.json` when running the application locally.

## 🧪 Testing

```bash
dotnet test
```

## 📊 Roadmap

- [ ] Basic user authentication
- [ ] Text messaging
- [ ] Group chats
- [ ] File sharing
- [ ] Video calling
- [ ] AI-powered features

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📜 License

This project is licensed under the [MIT License](LICENSE)

## 👥 Contact

Project Link: [https://github.com/Hadi-bakhshi/peyghom](https://github.com/Hadi-bakhshi/Peyghom)

---

Made with ❤️ by Hadi Bakhshi
