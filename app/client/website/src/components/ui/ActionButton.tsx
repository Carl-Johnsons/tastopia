import { ReactNode } from "react";

type ActionButtonProps = {
  onClick?: () => void;
  text: string;
  icon?: ReactNode;
  color?: "orange" | "green" | "red" | "blue" | "gray";
  isLoading?: boolean;
  disabled?: boolean;
  type?: "button" | "submit" | "reset";
};

export default function ActionButton({ onClick, text, icon, color = "orange", isLoading = false, disabled = false }: ActionButtonProps) {
  const colorClasses = {
    orange: "bg-orange-500 hover:bg-orange-600 text-white",
    green: "bg-green-600 hover:bg-green-700 text-white",
    red: "bg-red-500 hover:bg-red-600 text-white",
    blue: "bg-blue-500 hover:bg-blue-600 text-white",
    gray: "bg-gray-200 hover:bg-gray-300 text-gray-800",
  };

  return (
    <button
      onClick={onClick}
      className={`flex items-center justify-center gap-2 rounded-lg px-4 py-2 font-medium transition ${colorClasses[color]}`}
      disabled={disabled || isLoading}
    >
      {isLoading ? (
        <>
          <svg className="size-5 animate-spin" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
            <path
              className="opacity-75"
              fill="currentColor"
              d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
            ></path>
          </svg>
          Loading...
        </>
      ) : (
        <>
          {icon}
          {text}
        </>
      )}
    </button>
  );
}
