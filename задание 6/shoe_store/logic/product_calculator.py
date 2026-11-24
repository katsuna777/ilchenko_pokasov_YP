"""Расчет показателей товара для каталога."""

from datetime import timedelta

DISCOUNT_PERCENT = 25
MANY_QUANTITY_THRESHOLD = 5


def calculate_discounted_price(price, order_dates, calculation_date):
    """Возвращает цену товара с учетом скидки.

    Скидка 25% предоставляется, если в предыдущем календарном месяце
    (относительно даты расчета) по товару не было заказов.

    :param price: цена товара в рублях
    :param order_dates: список дат заказов товара (datetime.date), может быть пустым
    :param calculation_date: дата расчета (datetime.date)
    :return: цена с учетом скидки, округленная до копеек
    """
    if price < 0:
        raise ValueError("Цена не может быть отрицательной.")

    current_month_start = calculation_date.replace(day=1)
    previous_month_start = (current_month_start - timedelta(days=1)).replace(day=1)
    has_orders_in_previous_month = any(
        previous_month_start <= order_date < current_month_start
        for order_date in order_dates
    )

    if has_orders_in_previous_month:
        return price
    return round(price * (100 - DISCOUNT_PERCENT) / 100, 2)


def get_quantity_label(total_quantity):
    """Возвращает «много», если пар больше пяти, иначе «мало».

    :param total_quantity: количество пар суммарно по всем размерам
    """
    if total_quantity < 0:
        raise ValueError("Количество не может быть отрицательным.")
    if total_quantity > MANY_QUANTITY_THRESHOLD:
        return "много"
    return "мало"
