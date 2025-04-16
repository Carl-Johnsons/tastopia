"use client";

import { useGetAdminById } from "@/api/admin";
import ActivityFeed from "@/components/screen/admins/ActivityFeed";
import ProfileHeader from "@/components/screen/admins/ProfileHeader";
import ProfileInfo from "@/components/screen/admins/ProfileInfo";
import SomethingWentWrong from "@/components/shared/common/Error";
import { Link } from "@/i18n/navigation";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { useTranslations } from "next-intl";
import Loading from "../../users/[id]/loading";

export default function Page({ params }: ParamsProps) {
  const { id } = params;
  const t = useTranslations("administerAdmins");
  const { data: currentUser, isError, isLoading } = useGetAdminById(id);

  if (isError) {
    return <SomethingWentWrong />;
  }

  if (isLoading || !currentUser) {
    return <Loading />;
  }

  return (
    <div className='min-h-screen rounded-lg p-2'>
      <div className='mb-4 flex gap-2 self-start'>
        <Link href='/admins'>
          <span className='text-gray-500'>{t("title")}</span>
        </Link>
        <ChevronRight className='text-black_white' />
        <span className='text-black_white'>{t("detail.info.title")}</span>
      </div>

      <div className='mx-auto max-w-[960px]'>
        <ProfileHeader
          admin={currentUser}
          isViewingAdmin
        />

        <div className='mt-6 flex flex-col-reverse gap-6 lg:flex-row'>
          <div className='flex-1'>{<ActivityFeed accountId={id} />}</div>

          <div className='flex flex-col space-y-6 lg:w-1/3'>
            <ProfileInfo admin={currentUser} />
          </div>
        </div>
      </div>
    </div>
  );
}
