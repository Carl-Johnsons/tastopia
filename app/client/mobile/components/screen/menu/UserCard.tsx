import Button from "@/components/Button";
import Protected from "@/components/Protected";
import { ROLE } from "@/slices/auth.slice";
import { selectUser } from "@/slices/user.slice";
import { Avatar } from "@rneui/base";
import { Platform, Text } from "react-native";

type UserCardProps = {
  onPress: () => void;
};

export default function UserCard({ onPress }: UserCardProps) {
  const { displayName, avatarUrl } = selectUser();

  return (
    <Protected
      excludedRoles={[ROLE.GUEST]}
      forceDisplay
      requiredLogin
    >
      <Button
        style={Platform.select({
          ios: {
            shadowOffset: { width: 0, height: 2 },
            shadowOpacity: 0.25,
            shadowRadius: 50
          },
          android: { elevation: 20 }
        })}
        className='flex-row items-center gap-3 rounded-lg bg-white px-4 py-2 dark:bg-black-300'
        onPress={onPress}
      >
        <Avatar
          size={90}
          rounded
          source={
            avatarUrl ? { uri: avatarUrl } : require("../../../assets/images/avatar.png")
          }
          containerStyle={avatarUrl && { backgroundColor: "#FFC529" }}
        />
        <Text className='text-black_white font-semibold text-xl'>{displayName}</Text>
      </Button>
    </Protected>
  );
}
