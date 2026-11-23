# 📊 UrFU Deadline Tracker

Алгоритмический трекер академических дедлайнов и расчета текущего учебного рейтинга по лабораторным работам (`ulearn.me` / `OpenEdu`) для студентов ИРИТ-РТФ УрФУ.

## 🛠 Стек технологий
* **Core & Algorithms:** C# (.NET) — расчет индекса срочности задач и экспорт аналитики в CSV (`src/algo/Program.cs`)
* **Notifications:** Python — модуль формирования дайджестов и уведомлений в Telegram (`notifier/tg_reminder.py`)
* **Web Dashboard:** HTML5, CSS3, JavaScript — интерактивная сводка успеваемости (`report/index.html`)

## ⚡ Быстрый запуск
```bash
dotnet run --project src/algo
python3 notifier/tg_reminder.py
