class Renderer
{
    public void Draw(List<Layout> layouts)
    {
        foreach (var layout in layouts)
        {
            if (layout.Show) { Draw(layout); }
        }
    }
    public void Draw(Layout layout)
    {
        if (layout.ShowBorder) { RenderBorder(layout); }
        if (layout.ShowHeader) { RenderHeader(layout.Header); }
        if (layout.Content.Layouts.Count > 0)
        {
            foreach (var subLayout in layout.Content.Layouts)
            {
                if (subLayout.Show) { Draw(subLayout); }
            }
        }

        if (layout.Content.Rows != null)
        {
            if (layout.Content.Rows.Count > 0)
            {
                Console.SetCursorPosition(layout.Content.Position.Column, layout.Content.Position.Row);
                foreach (var row in layout.Content.Rows)
                {
                    Console.Write(row);
                    Console.SetCursorPosition(layout.Content.Position.Column, Console.CursorTop + 1);
                }
            }
        }

        void RenderBorder(Box box)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = box.BorderColor;
            if (box.Border.Left == 0 && box.Border.Top == 0 && box.Border.Right == 0 && box.Border.Bottom == 0)
            {
                return;
            }
            string Repeat(char c, int n)
            {
                return new string(c, n);
            }

            if (box.Border.Left == 1)
            {
                for (var i = 1; i < box.Size.Height - 1; i++)
                {
                    Console.SetCursorPosition(box.Position.Column, box.Position.Row + i);
                    Console.Write(box.Border.Style["Left"]);
                }
            }
            if (box.Border.Top == 1)
            {
                Console.SetCursorPosition(box.Position.Column, box.Position.Row);
                Console.Write(
                    box.Border.Left == 1 ?
                    box.Border.Style["TopLeft"] : box.Border.Style["Top"]
                    );
                Console.Write(Repeat(box.Border.Style["Top"], box.Size.Width - 2));
                Console.Write(
                    box.Border.Right == 1 ?
                    box.Border.Style["TopRight"] : box.Border.Style["Top"]
                    );
            }
            if (box.Border.Right == 1)
            {
                for (var i = 1; i < box.Size.Height - 1; i++)
                {
                    Console.SetCursorPosition(box.Position.Column + box.Size.Width - 1, box.Position.Row + i);
                    Console.Write(box.Border.Style["Right"]);
                }
            }
            if (box.Border.Bottom == 1)
            {
                Console.SetCursorPosition(box.Position.Column, box.Position.Row + box.Size.Height - 1);
                Console.Write(
                    box.Border.Left == 1 ?
                    box.Border.Style["BottomLeft"] : box.Border.Style["Bottom"]
                    );
                Console.Write(Repeat(box.Border.Style["Bottom"], box.Size.Width - 2));
                Console.Write(
                    box.Border.Right == 1 ?
                    box.Border.Style["BottomRight"] : box.Border.Style["Bottom"]
                    );
            }

        }

        void RenderHeader(Header header)
        {
            Console.BackgroundColor = header.BackgroundColor;
            Console.ForegroundColor = header.TextColor;
            Console.SetCursorPosition(header.Position.Column, header.Position.Row);
            foreach (var row in header.Content)
            {
                Console.Write(row);
                Console.SetCursorPosition(header.Position.Column, Console.CursorTop + 1);
            }
        }
    }
    public void UpdateContent(Content content)
    {
        content.UpdatePosition();
        Console.BackgroundColor = content.Parent.BackgroundColor;
        Console.ForegroundColor = content.Parent.TextColor;

        for (var i = 0; i < content.Rows.Count; i++)
        {
            Console.SetCursorPosition(content.Position.Column, content.Position.Row + i);
            // Console.Write(new string(' ', content.Parent.Size.Width - content.Size.Width));
            // Console.SetCursorPosition(content.Position.Column, content.Position.Row + i);
            Console.Write(content.Rows[i]);
        }
        // Console.BackgroundColor = ConsoleColor.Black;
        // Console.ForegroundColor = ConsoleColor.White;
    }
    public void UpdateContent(Content content, List<int> indexesToUpdate)
    {
        content.UpdatePosition();
        Console.BackgroundColor = content.Parent.BackgroundColor;
        Console.ForegroundColor = content.Parent.TextColor;
        for (var i = 0; i < indexesToUpdate.Count; i++)
        {
            Console.SetCursorPosition(content.Position.Column, content.Position.Row + indexesToUpdate[i]);
            Console.Write(new string(' ', content.Parent.Size.Width - content.Size.Width));
            Console.SetCursorPosition(content.Position.Column, content.Position.Row + indexesToUpdate[i]);
            Console.Write(content.Rows[indexesToUpdate[i]]);
        }
        // Console.BackgroundColor = ConsoleColor.Black;
        // Console.ForegroundColor = ConsoleColor.White;
    }
    public void UpdateContent(Content content, int indexToUpdate)
    {
        content.UpdatePosition();

        Console.BackgroundColor = content.Parent.BackgroundColor;
        Console.ForegroundColor = content.Parent.TextColor;

        Console.SetCursorPosition(content.Position.Column, content.Position.Row + indexToUpdate);
        Console.Write(new string(' ', content.Parent.Size.Width));
        Console.SetCursorPosition(content.Position.Column, content.Position.Row + indexToUpdate);
        Console.Write(content.Rows[indexToUpdate]);
    }
    public void ClearContent(Layout layout)
    {
        Console.BackgroundColor = layout.BackgroundColor;
        Console.ForegroundColor = layout.TextColor;
        for (int i = layout.Position.Row + 1; i < layout.Position.Row + layout.Size.Height - 1; i++)
        {
            for (var j = layout.Position.Column + 1; j < layout.Position.Column + layout.Size.Width - 1; j++)
            {
                Console.SetCursorPosition(j, i);
                Console.Write(' ');
            }
        }
        // Console.BackgroundColor = ConsoleColor.Black;
        // Console.ForegroundColor = ConsoleColor.White;
    }
    public void ShowContent(object content, Layout layout)
    {
        layout.Content ??= new();
        layout.Content.Parent ??= layout;
        layout.Content.Rows ??= new();

        var list = content as List<dynamic>;
        foreach (var item in list)
        {
            layout.Content.Rows.Add(item.label);
        }
        UpdateContent(layout.Content);
    }
    public void Print(string message, Layout layout)
    {
        layout.Content.Rows.Add(message);
        UpdateContent(layout.Content);
    }
    public static void Hide(Layout layout)
    {
        layout.Show = false;
        Console.BackgroundColor = layout.Parent.BackgroundColor;
        Console.ForegroundColor = layout.Parent.TextColor;
        for (var row = layout.Position.Row; row < layout.Position.Row + layout.Size.Height; row++)
        {
            for (var column = layout.Position.Column; column < layout.Position.Column + layout.Size.Width; column++)
            {
                Console.SetCursorPosition(column, row);
                Console.Write(' ');
            }
        }
        for (var row = layout.Header.Position.Row; row < layout.Header.Position.Row + layout.Header.Size.Height; row++)
        {
            for (var column = layout.Header.Position.Column; column < layout.Header.Position.Column + layout.Header.Size.Width; column++)
            {
                Console.SetCursorPosition(column, row);
                Console.Write(' ');
            }
        }
    }
}
