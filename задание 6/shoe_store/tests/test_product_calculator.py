from datetime import date

from logic.product_calculator import calculate_discounted_price, get_quantity_label


def test_price_without_orders_in_previous_month_applies_discount():
    result = calculate_discounted_price(2000, [], date(2027, 6, 15))

    assert result == 1500


def test_price_with_order_in_previous_month_remains_unchanged():
    result = calculate_discounted_price(2000, [date(2027, 5, 10)], date(2027, 6, 15))

    assert result == 2000


def test_quantity_of_five_pairs_is_few():
    assert get_quantity_label(5) == "мало"


def test_quantity_of_six_pairs_is_many():
    assert get_quantity_label(6) == "много"
