import { format } from "date-fns";

export enum TimeFormat {
  ddMMyyyy = "dd-MM-yyyy",
  yyyyMMdd = "yyyy-MM-dd"
}

export const formatDate = (dateTimeString: any | null) => {
  if (!dateTimeString) return ""; // Handle empty or undefined values
  const date = new Date(dateTimeString);

  return format(date, "dd-MM-yyyy");
};

export const formatDateForTaskPage = (dateTimeString: string | null) => {
  if (!dateTimeString) return ""; // Handle empty or undefined values

  const date = new Date(dateTimeString);
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const day = date.getDate().toString().padStart(2, "0");

  return `${year}-${month}-${day}`;
};

export const formatDateCustom = (
  dateTimeString: string | null,
  format = TimeFormat.yyyyMMdd
) => {
  if (!dateTimeString) return ""; // Handle empty or undefined values

  const date = new Date(dateTimeString);
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const day = date.getDate().toString().padStart(2, "0");

  return format === TimeFormat.yyyyMMdd
    ? `${year}-${month}-${day}`
    : `${day}-${month}-${year}`;
};
