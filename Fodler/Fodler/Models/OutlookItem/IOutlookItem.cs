using Microsoft.Office.Interop.Outlook;

namespace Fodler.Models.OutlookItem
{
    public interface IOutlookItem
    {
        void Move(Folder folder);
        string[] GetInput();
        string[] GetSubject();
        string[] GetText();
        string GetStoreId();
    }
}
