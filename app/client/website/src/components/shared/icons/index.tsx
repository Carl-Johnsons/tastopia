import { SvgIconProps } from "@/types/icon";
import React from "react";

const RestoreIcon = () => {
  return (
    <svg
      xmlns='http://www.w3.org/2000/svg'
      className='size-5'
      viewBox='0 0 20 20'
      fill='currentColor'
    >
      <path
        fillRule='evenodd'
        d='M4 2a1 1 0 011 1v2.101a7.002 7.002 0 0111.601 2.566 1 1 0 11-1.885.666A5.002 5.002 0 005.999 7H9a1 1 0 010 2H4a1 1 0 01-1-1V3a1 1 0 011-1zm.008 9.057a1 1 0 011.276.61A5.002 5.002 0 0014.001 13H11a1 1 0 110-2h5a1 1 0 011 1v5a1 1 0 11-2 0v-2.101a7.002 7.002 0 01-11.601-2.566 1 1 0 01.61-1.276z'
        clipRule='evenodd'
      />
    </svg>
  );
};

const RecipeIcon = () => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className='size-4'
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M9 2a1 1 0 000 2h2a1 1 0 100-2H9z' />
    <path
      fillRule='evenodd'
      d='M4 5a2 2 0 012-2 3 3 0 003 3h2a3 3 0 003-3 2 2 0 012 2v11a2 2 0 01-2 2H6a2 2 0 01-2-2V5zm3 4a1 1 0 000 2h.01a1 1 0 100-2H7zm3 0a1 1 0 000 2h3a1 1 0 100-2h-3zm-3 4a1 1 0 100 2h.01a1 1 0 100-2H7zm3 0a1 1 0 100 2h3a1 1 0 100-2h-3z'
      clipRule='evenodd'
    />
  </svg>
);

const BanIcon = ({ className }: SvgIconProps) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={`size-4 ${className}`}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M13.477 14.89A6 6 0 015.11 6.524l8.367 8.368zm1.414-1.414L6.524 5.11a6 6 0 018.367 8.367zM18 10a8 8 0 11-16 0 8 8 0 0116 0z'
      clipRule='evenodd'
    />
  </svg>
);

const CommentIcon = () => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className='size-4'
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M18 10c0 3.866-3.582 7-8 7a8.841 8.841 0 01-4.083-.98L2 17l1.338-3.123C2.493 12.767 2 11.434 2 10c0-3.866 3.582-7 8-7s8 3.134 8 7zM7 9H5v2h2V9zm8 0h-2v2h2V9zM9 9h2v2H9V9z'
      clipRule='evenodd'
    />
  </svg>
);

const DefaultIcon = () => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className='size-4'
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-6-3a2 2 0 11-4 0 2 2 0 014 0zm-2 4a5 5 0 00-4.546 2.916A5.986 5.986 0 0010 16a5.986 5.986 0 004.546-2.084A5 5 0 0010 11z'
      clipRule='evenodd'
    />
  </svg>
);

const ArrowDownIcon = () => (
  <svg
    className='size-4 fill-current'
    xmlns='http://www.w3.org/2000/svg'
    viewBox='0 0 20 20'
  >
    <path d='M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z' />
  </svg>
);

const LoadingIcon = ({ className }: SvgIconProps) => (
  <svg
    className={`size-5 animate-spin text-white ${className}`}
    xmlns='http://www.w3.org/2000/svg'
    fill='none'
    viewBox='0 0 24 24'
  >
    <circle
      className='opacity-25'
      cx='12'
      cy='12'
      r='10'
      stroke='currentColor'
      strokeWidth='4'
    ></circle>
    <path
      className='opacity-75'
      fill='currentColor'
      d='M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z'
    ></path>
  </svg>
);

const SaveIcon = () => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className='size-5'
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z'
      clipRule='evenodd'
    />
  </svg>
);

const UserIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z'
      clipRule='evenodd'
    />
  </svg>
);

const EmailIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M2.003 5.884L10 9.882l7.997-3.998A2 2 0 0016 4H4a2 2 0 00-1.997 1.884z' />
    <path d='M18 8.118l-8 4-8-4V14a2 2 0 002 2h12a2 2 0 002-2V8.118z' />
  </svg>
);

const PhoneIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M2 3a1 1 0 011-1h2.153a1 1 0 01.986.836l.74 4.435a1 1 0 01-.54 1.06l-1.548.773a11.037 11.037 0 006.105 6.105l.774-1.548a1 1 0 011.059-.54l4.435.74a1 1 0 01.836.986V17a1 1 0 01-1 1h-2C7.82 18 2 12.18 2 5V3z' />
  </svg>
);

const GenderIcon = ({ className = "size-6" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    viewBox='0 0 24 24'
    fill='none'
    stroke='currentColor'
    stroke-width='2'
    strokeLinecap='round'
    strokeLinejoin='round'
    className={`${className}`}
  >
    <path
      stroke='none'
      d='M0 0h24v24H0z'
      fill='none'
    />
    <path d='M11 11m-4 0a4 4 0 1 0 8 0a4 4 0 1 0 -8 0' />
    <path d='M19 3l-5 5' />
    <path d='M15 3h4v4' />
    <path d='M11 16v6' />
    <path d='M8 19h6' />
  </svg>
);

const CalendarIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z'
      clipRule='evenodd'
    />
  </svg>
);

const LocationIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z'
      clipRule='evenodd'
    />
  </svg>
);

const FollowersIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M13 6a3 3 0 11-6 0 3 3 0 016 0zM18 8a2 2 0 11-4 0 2 2 0 014 0zM14 15a4 4 0 00-8 0v3h8v-3zM6 8a2 2 0 11-4 0 2 2 0 014 0zM16 18v-3a5.972 5.972 0 00-.75-2.906A3.005 3.005 0 0119 15v3h-3zM4.75 12.094A5.973 5.973 0 004 15v3H1v-3a3 3 0 013.75-2.906z' />
  </svg>
);

const FollowingIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M8 9a3 3 0 100-6 3 3 0 000 6zM8 11a6 6 0 016 6H2a6 6 0 016-6zM16 7a1 1 0 10-2 0v1h-1a1 1 0 100 2h1v1a1 1 0 102 0v-1h1a1 1 0 100-2h-1V7z' />
  </svg>
);

const RecipesIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path d='M9 2a1 1 0 000 2h2a1 1 0 100-2H9z' />
    <path
      fillRule='evenodd'
      d='M4 5a2 2 0 012-2 3 3 0 003 3h2a3 3 0 003-3 2 2 0 012 2v11a2 2 0 01-2 2H6a2 2 0 01-2-2V5zm3 4a1 1 0 000 2h.01a1 1 0 100-2H7zm3 0a1 1 0 000 2h3a1 1 0 100-2h-3zm-3 4a1 1 0 100 2h.01a1 1 0 100-2H7zm3 0a1 1 0 100 2h3a1 1 0 100-2h-3z'
      clipRule='evenodd'
    />
  </svg>
);

const TimeIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={className}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M10 18a8 8 0 100-16 8 8 0 000 16zm1-12a1 1 0 10-2 0v4a1 1 0 00.293.707l2.828 2.829a1 1 0 101.415-1.415L11 9.586V6z'
      clipRule='evenodd'
    />
  </svg>
);

const InfoIcon = ({ className = "size-5" }) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    viewBox='0 0 24 24'
    fill='none'
    stroke='currentColor'
    stroke-width='2'
    strokeLinecap='round'
    strokeLinejoin='round'
    className={className}
  >
    <path
      stroke='none'
      d='M0 0h24v24H0z'
      fill='none'
    />
    <path d='M19.875 6.27c.7 .398 1.13 1.143 1.125 1.948v7.284c0 .809 -.443 1.555 -1.158 1.948l-6.75 4.27a2.269 2.269 0 0 1 -2.184 0l-6.75 -4.27a2.225 2.225 0 0 1 -1.158 -1.948v-7.285c0 -.809 .443 -1.554 1.158 -1.947l6.75 -3.98a2.33 2.33 0 0 1 2.25 0l6.75 3.98h-.033z' />
    <path d='M12 9h.01' />
    <path d='M11 12h1v4h1' />
  </svg>
);

const UpvoteIcon = ({ className }: SvgIconProps) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={`size-4 ${className}`}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M3.293 9.707a1 1 0 010-1.414l6-6a1 1 0 011.414 0l6 6a1 1 0 01-1.414 1.414L11 5.414V17a1 1 0 11-2 0V5.414L4.707 9.707a1 1 0 01-1.414 0z'
      clipRule='evenodd'
    />
  </svg>
);

const DownvoteIcon = ({ className }: SvgIconProps) => (
  <svg
    xmlns='http://www.w3.org/2000/svg'
    className={`size-4 ${className}`}
    viewBox='0 0 20 20'
    fill='currentColor'
  >
    <path
      fillRule='evenodd'
      d='M16.707 10.293a1 1 0 010 1.414l-6 6a1 1 0 01-1.414 0l-6-6a1 1 0 111.414-1.414L9 14.586V3a1 1 0 012 0v11.586l4.293-4.293a1 1 0 011.414 0z'
      clipRule='evenodd'
    />
  </svg>
);

const SignOutIcon = ({ className }: SvgIconProps) => (
  <svg
    fill='none'
    height='24'
    viewBox='0 0 24 24'
    width='24'
    xmlns='http://www.w3.org/2000/svg'
  >
    <path
      d='M17 16L21 12M21 12L17 8M21 12L7 12M13 16V17C13 18.6569 11.6569 20 10 20H6C4.34315 20 3 18.6569 3 17V7C3 5.34315 4.34315 4 6 4H10C11.6569 4 13 5.34315 13 7V8'
      stroke='#374151'
      stroke-linecap='round'
      stroke-linejoin='round'
      stroke-width='2'
    />
  </svg>
);

export {
  RestoreIcon,
  RecipeIcon,
  CommentIcon,
  BanIcon,
  DefaultIcon,
  ArrowDownIcon,
  LoadingIcon,
  SaveIcon,
  UserIcon,
  PhoneIcon,
  RecipesIcon,
  CalendarIcon,
  LocationIcon,
  FollowersIcon,
  FollowingIcon,
  EmailIcon,
  GenderIcon,
  TimeIcon,
  InfoIcon,
  UpvoteIcon,
  DownvoteIcon,
  SignOutIcon
};
