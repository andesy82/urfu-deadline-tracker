import os
import asyncio
import httpx

BOT_TOKEN = os.getenv("TELEGRAM_BOT_TOKEN", "YOUR_BOT_TOKEN")
CHAT_ID = os.getenv("TELEGRAM_CHAT_ID", "YOUR_CHAT_ID")


def format_deadline_alert(subject: str, lab_name: str, deadline_str: str, hours_left: int) -> str:
    """Формирует текст уведомления в зависимости от срочности дедлайна."""
    urgency = "🚨 СРОЧНО" if hours_left <= 24 else "⏳ Напоминание"
    return (
        f"{urgency}: <b>Горящий дедлайн УрФУ!</b>\n\n"
        f"📚 <b>Предмет:</b> {subject}\n"
        f"📝 <b>Работа:</b> {lab_name}\n"
        f"📅 <b>Сдать до:</b> {deadline_str} (осталось ~{hours_left} ч.)"
    )


async def send_deadline_reminder(subject: str, lab_name: str, deadline_str: str, hours_left: int) -> bool:
    """Отправляет напоминание о лабе в Telegram."""
    message_text = format_deadline_alert(subject, lab_name, deadline_str, hours_left)

    if BOT_TOKEN == "YOUR_BOT_TOKEN":
        print(f"[MOCK REMINDER]\n{message_text}\n")
        return True

    url = f"https://telegram.org{BOT_TOKEN}/sendMessage"
    payload = {
        "chat_id": CHAT_ID,
        "text": message_text,
        "parse_mode": "HTML"
    }

    async with httpx.AsyncClient() as client:
        response = await client.post(url, json=payload)
        return response.status_code == 200


if __name__ == "__main__":
    test_tasks = [
        ("Программирование на C# (ulearn)", "Практика: Циклы и массивы", "24.11.2026 23:59", 18),
        ("Алгоритмы и структуры данных", "Лабораторная работа №3", "26.11.2026 18:00", 60),
    ]
    for subj, lab, dl, hrs in test_tasks:
        asyncio.run(send_deadline_reminder(subj, lab, dl, hrs))
