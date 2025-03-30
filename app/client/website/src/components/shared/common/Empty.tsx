import { useTranslations } from "next-intl";
import React from "react";

export const Empty = ({
  title = "",
  description = "",
  icon = "list",
  actionButton = null
}) => {
  const t = useTranslations("component");
  title = title || t("empty.title");
  description = description || t("empty.description");
  const renderIcon = () => {
    switch (icon) {
      case "activity":
        return (
          <svg
            className='mx-auto size-20 text-gray-300 dark:text-gray-600'
            fill='none'
            stroke='currentColor'
            viewBox='0 0 24 24'
            xmlns='http://www.w3.org/2000/svg'
          >
            <path
              strokeLinecap='round'
              strokeLinejoin='round'
              strokeWidth='1.5'
              d='M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z'
            ></path>
          </svg>
        );
      case "list":
      default:
        return (
          <svg
            className='mx-auto size-20 text-gray-300 dark:text-gray-600'
            fill='none'
            stroke='currentColor'
            viewBox='0 0 24 24'
            xmlns='http://www.w3.org/2000/svg'
          >
            <path
              strokeLinecap='round'
              strokeLinejoin='round'
              strokeWidth='1.5'
              d='M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2'
            ></path>
          </svg>
        );
    }
  };

  return (
    <div className='flex min-h-[40vh] flex-col items-center justify-center px-4 py-12'>
      <div className='mb-6'>{renderIcon()}</div>
      <h3 className='mb-2 text-xl font-medium text-gray-700 dark:text-gray-200'>
        {title}
      </h3>
      <p className='mb-6 max-w-md text-center text-gray-500 dark:text-gray-400'>
        {description}
      </p>
      {actionButton && <div>{actionButton}</div>}
    </div>
  );
};

export default Empty;
