# Bereza Shop (WPF)

> Graphical WPF application for managing a shop bill — KhAI summer practice, group **611п**.

Repository: [GITHUB_USERNAME/BerezaShop](https://github.com/GITHUB_USERNAME/BerezaShop)

## Features

| # | Feature / UI Action | Validation & Description |
|---|----------------------|--------------------------|
| 1 | **Add Item** | Description 3–20 chars, price > 0, max **5** items |
| 2 | **Remove Selected** | Removes item selected in `ListView` |
| 3 | **Apply Tip** | Percentage or fixed amount; automatically resets if bill is emptied |
| 4 | **Totals Calculation** | Real-time calculation: Net Total, Tip, GST (5%), and Grand Total |
| 5 | **Clear All** | Resets all items and tip amount |
| 6 | **Save File** | Export to CSV via `SaveFileDialog` |
| 7 | **Load File** | Import items from CSV via `OpenFileDialog`; validates max 5 items |

The application **never crashes** on invalid input or unhandled runtime errors — every action is guarded with localized `try-catch` blocks and input sanitization (`decimal.TryParse` with both `.` and `,` support). Global application safety is guaranteed via `DispatcherUnhandledException` in `App.xaml.cs`.

## Architecture

WPF desktop application built on **.NET 8** (`net8.0-windows`):

- **`MainWindow.xaml`** — User Interface layout: styled AliceBlue background (`#F0F8FF`), structured `GroupBox` containers, `ListView` for bill items display, and a dedicated totals panel.
- **`MainWindow.xaml.cs`** — Core UI logic and state handling:
  - `ObservableCollection<MenuItem>` — dynamic data binding for real-time `ListView` updates.
  - Event handlers for all buttons (`BtnAdd`, `BtnRemove`, `BtnApplyTip`, `BtnClear`, `BtnSave`, `BtnLoad`).
  - `UpdateTotals()` — centralized calculation for Net, GST, Tip, and Total amounts.
- **`App.xaml.cs`** — Top-level guard: hooks into `DispatcherUnhandledException` and `AppDomain.CurrentDomain.UnhandledException` to capture unexpected system exceptions without application termination.
- **`MenuItem`** — Data model class representing single bill items (`Description`, `Price`).

## How to run

```bash
dotnet run