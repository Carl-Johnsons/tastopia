import { ParamsProps } from "@/types/link";
import { getUserActivitiesById, getUserById } from "@/actions/user.action";
import ProfileHeader from "@/components/screen/profile/ProfileHeader";
import ActivityFeed from "@/components/screen/profile/ActivityFeed";
import ProfileInfo from "@/components/screen/profile/ProfileInfo";
import SomeThingWentWrong from "@/components/shared/common/Error";
import ReportList from "@/components/screen/report/user/ReportList";

export default async function ReportedDetail({ params }: ParamsProps) {
  try {
    const { ok: userOk, data: currentUser } = await getUserById(params.id);
    const { ok: activityOk, data: currentUserActivities } = await getUserActivitiesById(
      params.id,
      "en"
    );

    if (!userOk || !activityOk) {
      throw new Error("Failed to fetch data");
    }

    return (
      <div className='min-h-screen rounded-lg p-2'>
        <div className='container'>
          <ProfileHeader user={currentUser} />

          <div className='mt-6 flex flex-col-reverse gap-6 xl:flex-row'>
            <div className='flex-1'>
              {currentUserActivities && (
                <ActivityFeed activities={currentUserActivities.paginatedData} />
              )}
            </div>

            <div className='flex flex-col space-y-6 xl:w-1/3'>
              <ReportList reportedId={params.id} />
              <ProfileInfo user={currentUser} />
            </div>
          </div>
        </div>
      </div>
    );
  } catch (error) {
    return <SomeThingWentWrong />;
  }
}
