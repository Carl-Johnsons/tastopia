// "use client";

// import { Button } from "@/components/ui/button";
// import { Clock, X } from "lucide-react";
// import StatusText from "../common/StatusText";
// import { ReportStatus } from "@/constants/reports";
// import { format } from "date-fns";
// import { ReactNode, useContext, useState } from "react";
// import Loader from "@/components/ui/Loader";
// import {
//   DisableCommentButton,
//   MarkAsCompletedButton,
//   ReopenReportButton,
//   RestoreCommentButton
// } from "./Button";
// import { DataTableContext } from "./Provider";
// import useWindowDimensions from "@/hooks/useWindowDimensions";
// import { IAdminReportCommentResponse, IRecipe } from "@/generated/interfaces/recipe.interface";
// import { faker } from "@faker-js/faker";
// import { IUser } from "@/generated/interfaces/user.interface";
// import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
// import Image from "next/image";

// type Props = {
//   report: IAdminReportCommentResponse | undefined;
//   className?: string;
//   isOpen: boolean;
//   onClose: () => void;
// };

// const PADDING_Y = 50;

// export default function ReportDetailModal({ report, className, isOpen }: Props) {
//   if (!isOpen) return null;
//   console.log("report", report);
//   if (!report) {
//     return (
//       <Modal>
//         <Loader />
//       </Modal>
//     );
//   }

//   const {
//     recipeId,
//     reportId,
//     commentId,
//     reporterUsername,
//     reportReason,
//     createdAt,
//     commentContent,
//   } = report;
//   const formatedCreatedDate = format(new Date(createdAt), "HH:mm yyyy-MM-dd");
//   const { onCloseModal } = useContext(DataTableContext) as DataTableContext;
//   const { height } = useWindowDimensions();

//   const [isCommentActive, setIsCommentActive] = useState(report.isCommentActive);
//   const [isReportActive, setIsReportActive] = useState(
//     report.status === ReportStatus.Pending
//   );

//   return (
//     <Modal onClickOutside={onCloseModal}>
//       <div
//         className={`flex flex-col gap-2 overflow-y-scroll rounded-xl border border-gray-200 bg-white p-6 shadow-sm dark:border-gray-600 dark:bg-black-500 ${className}`}
//         style={{ maxHeight: height - 2 * PADDING_Y }}
//       >
//         <div className='flex items-center justify-between'>
//           <div />
//           <div className='flex gap-2'>
//             <StatusText
//               status={isReportActive ? ReportStatus.Pending : ReportStatus.Done}
//               coloring
//             />

//             <Button
//               onClick={onCloseModal}
//               className='h-[40px] w-[40px] rounded-full bg-gray-100 dark:bg-black-100 dark:hover:opacity-50'
//             >
//               <X className='text-black_white' />
//             </Button>
//           </div>
//         </div>
//         <div className='flex flex-col gap-2'>
//           <Section
//             title='Username'
//             content={reporterUsername}
//           />
//           <Section
//             title='Report Reason'
//             content={reportReason}
//           />
//           <Section
//             title='Created Date'
//             content={formatedCreatedDate}
//           />
//           <Section
//             title='Comment content'
//             content={commentContent}
//           />
//           <Section title='Recipe link'>
//             <RecipePreview recipeId={recipeId} />
//           </Section>
//         </div>
//         <div className='mt-4 flex items-center justify-between'>
//           {isCommentActive ? (
//             <DisableCommentButton
//               title='Disable comment'
//               targetId={commentId}
//               recipeId={recipeId}
//               onSuccess={() => setIsCommentActive(false)}
//             />
//           ) : (
//             <RestoreCommentButton
//               title='Restore comment'
//               targetId={commentId}
//               recipeId={recipeId}
//               onSuccess={() => setIsCommentActive(true)}
//             />
//           )}
//           {isReportActive ? (
//             <MarkAsCompletedButton
//               title='Mark as completed'
//               targetId={reportId}
//               onSuccess={() => setIsReportActive(false)}
//               noText={false}
//             />
//           ) : (
//             <ReopenReportButton
//               title='Reopen report'
//               targetId={reportId}
//               onSuccess={() => setIsReportActive(true)}
//               noText={false}
//             />
//           )}
//         </div>
//       </div>
//     </Modal>
//   );
// }

// const Section = ({
//   title,
//   content,
//   children
// }: {
//   title: string;
//   content?: string;
//   children?: ReactNode;
// }) => {
//   return (
//     <div className='flex flex-col gap-2'>
//       <h3 className='text-black_white font-medium'>{title}</h3>
//       {!!content && (
//         <div className='rounded-lg bg-gray-100 px-4 py-2 dark:bg-black-100'>
//           <p className='text-black dark:text-gray-100'>{content}</p>
//         </div>
//       )}
//       {children}
//     </div>
//   );
// };

// const RecipePreview = ({ recipeId }: { recipeId: string }) => {
//   const { title, imageUrl, createdAt }: Partial<IRecipe> = {
//     title: faker.lorem.paragraph(),
//     imageUrl: faker.image.urlLoremFlickr({ width: 400, height: 200 }),
//     createdAt: faker.date.recent().toString()
//   };

//   const formatedCreatedDate = format(new Date(createdAt as string), "HH:mm yyyy-MM-dd");

//   const { displayName, avatarUrl }: Partial<IUser> = {
//     displayName: faker.name.fullName(),
//     avatarUrl: faker.image.avatar()
//   };

//   return (
//     <div className='flex flex-col gap-4 rounded-lg border border-gray-200 p-4 dark:border-gray-600'>
//       <p className='text-black_white'>{title}</p>
//       <div className='flex items-center gap-2'>
//         <Avatar>
//           <AvatarImage src={avatarUrl} />
//           <AvatarFallback className='bg-black_white text-white_black'>
//             {displayName.substring(0, 1)}
//           </AvatarFallback>
//         </Avatar>
//         <span className='text-black_white font-bold'>{displayName}</span>
//       </div>
//       <div className='flex gap-2 text-gray-700 dark:text-gray-400'>
//         <Clock />
//         <span className='text-sm'>{formatedCreatedDate}</span>
//       </div>
//       <Image
//         src={imageUrl}
//         alt="Comment's recipe image"
//         width={400}
//         height={200}
//         className='text-black_white flex-center border-black_white h-[200px] w-full rounded-lg border object-cover'
//       />
//     </div>
//   );
// };

// const Modal = ({
//   children,
//   className,
//   wrapperClassName,
//   onClickOutside
// }: {
//   children: ReactNode;
//   className?: string;
//   wrapperClassName?: string;
//   onClickOutside?: () => void;
// }) => {
//   return (
//     <div
//       className={`fixed inset-0 left-0 top-0 z-[999] ${className}`}
//       style={{ paddingInline: PADDING_Y }}
//     >
//       <div
//         onClick={onClickOutside}
//         className='absolute inset-0 bg-black/50'
//       />
//       <div
//         className={`absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 ${wrapperClassName}`}
//       >
//         {children}
//       </div>
//     </div>
//   );
// };
