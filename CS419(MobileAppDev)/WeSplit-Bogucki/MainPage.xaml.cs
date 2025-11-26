namespace WeSplit_Bogucki
{
    public partial class MainPage : ContentPage
    {
        private double mealCost = 0;
        private int tipPercentage = 15; // Default to 15%
        private int numberOfPeople = 1; // Default to 1 person

        public MainPage()
        {
            InitializeComponent();
            TipPicker.SelectedIndex = 1; // Select 15% by default
            UpdatePeopleLabel();
        }

        private void OnMealCostChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double cost))
                mealCost = cost;
            else
                mealCost = 0;
        }

        private void OnTipChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                string selectedTip = picker.Items[picker.SelectedIndex];
                string tipValue = selectedTip.Replace("%", "");
                if (int.TryParse(tipValue, out int tip))
                    tipPercentage = tip;
            }
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            if (numberOfPeople > 1)
            {
                numberOfPeople--;
                UpdatePeopleLabel();
            }
        }

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            numberOfPeople++;
            UpdatePeopleLabel();
        }

        private void UpdatePeopleLabel()
        {
            if (numberOfPeople == 1)
                PeopleCountLabel.Text = "1 person";
            else
                PeopleCountLabel.Text = $"{numberOfPeople} people";
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            CalculatePerPersonAmount();
        }

        private void CalculatePerPersonAmount()
        {
            double tipAmount = mealCost * (tipPercentage / 100.0);
            double totalWithTip = mealCost + tipAmount;
            double perPersonAmount = totalWithTip / numberOfPeople;

            ResultLabel.Text = $"Each person pays: {perPersonAmount:C2}";
        }
    }
}