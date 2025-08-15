using System;

namespace UI.View
{
    public interface IMetaUIView
    {
        event Action OnSelectCharacterClicked;
        void SetSelectedText(string text);
    }
}