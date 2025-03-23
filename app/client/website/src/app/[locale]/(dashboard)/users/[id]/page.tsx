import { ParamsProps } from "@/types/link";
import { getUserActivitiesById, getUserById } from "@/actions/user.action";
import ProfileHeader from "@/components/screen/profile/ProfileHeader";
import ActivityFeed from "@/components/screen/profile/ActivityFeed";
import ProfileInfo from "@/components/screen/profile/ProfileInfo";
import SomeThingWentWrong from "@/components/shared/common/Error";
import { getLocale } from "next-intl/server";

export default async function UserProfile({ params }: ParamsProps) {
  try {
    const currentLanguage = await getLocale();
    const currentUser = await getUserById(params.id);
    const currentUserActivities = await getUserActivitiesById(params.id, currentLanguage);

    return (
      <div className='min-h-screen rounded-lg p-2'>
        <div className='mx-auto max-w-[96]'>
          <ProfileHeader user={currentUser} />

          <div className='mt-6 flex flex-col-reverse gap-6 lg:flex-row'>
            <div className='flex-1'>
              {currentUserActivities && (
                <ActivityFeed activities={currentUserActivities.paginatedData} />
              )}
            </div>

            <div className='flex flex-col space-y-6 lg:w-1/3'>
              <ProfileInfo user={currentUser} />
              {/* <ProfileSettings /> */}
            </div>
          </div>
        </div>
      </div>
    );
  } catch (error) {
    console.log(error);
    return <SomeThingWentWrong />;
  }
}
