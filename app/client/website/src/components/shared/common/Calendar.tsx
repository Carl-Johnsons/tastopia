import { DayPicker, DayPickerProps } from "react-day-picker";
import "react-day-picker/style.css";

export function Calendar(props: DayPickerProps) {
  return (
    <DayPicker
      animate
      classNames={{
        selected: "bg-primary text-white",
        day: "rounded-full",
        today: "text-primary",
        chevron: "fill-primary"
      }}
      {...props}
    />
  );
}
