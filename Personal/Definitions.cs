using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal
{
     class Definitions // класс определений и констант проекта Personal
    {
        // константы, используемые в работе кода с приложением
        
        public const string strPrefix = "пк"; // префикс для IDC_text

        // константы, используемые для работы с БД SQL

        public const string strQNewIDC = "SELECT ISNULL(MAX(IDC), 1) + 1 AS max_id FROM [dbo].[RegCounterparties]"; // выборка для нового возможного IDC (для регистрации)
        public const string strSpAddProperty = "sp_Add_property"; // процедура SQL для добавления свойств

        // константы, используемые для описаний всплывающих окон

        public const string strMsgRequiredFields = "Заполните все обязательные поля (они помечены красной звездочкой)"; // сообщение для всплывающего окна при незаполнении всех полей
        public const string strErrConnToDB = "Ошибка при подключении к базе данных, проверьте подключение или обратитесь к системному администратору. Описание ошибки: "; // сообщения для всплывающего окна при ошибке подключения
    }
}
