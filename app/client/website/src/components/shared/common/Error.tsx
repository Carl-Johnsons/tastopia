import React from "react";

export const SomethingWentWrong = () => {
  return (
    <div className='flex min-h-[80vh] w-full flex-col items-center justify-center bg-gradient-to-b px-4 text-center'>
      <div className='mb-6'>
        <svg
          className='mx-auto size-24 text-gray-400 dark:text-gray-500'
          fill='none'
          stroke='currentColor'
          viewBox='0 0 24 24'
          xmlns='http://www.w3.org/2000/svg'
        >
          <path
            strokeLinecap='round'
            strokeLinejoin='round'
            strokeWidth='1.5'
            d='M9.172 16.172a4 4 0 015.656 0M9 10h.01M15 10h.01M12 4c4.418 0 8 3.582 8 8s-3.582 8-8 8-8-3.582-8-8 3.582-8 8-8z'
          ></path>
        </svg>
      </div>
      <h1 className='mb-2 text-3xl font-bold text-gray-800 dark:text-gray-100'>Oops!</h1>
      <p className='mb-8 text-xl text-gray-600 dark:text-gray-300'>
        Something went wrong
      </p>
    </div>
  );
};

export default SomethingWentWrong;
