import { SafeAreaView, StyleSheet, Text, View } from "react-native";
import { TabView } from "@rneui/themed";

type CommentsTabProps = {
  accountId: string;
};

const CommentsTab = ({ accountId }: CommentsTabProps) => {
  return (
    <TabView.Item style={{ width: "100%" }}>
      <SafeAreaView
        style={{
          width: "100%",
          flex: 1,
          alignItems: "center"
        }}
      >
        <Text>comment</Text>
      </SafeAreaView>
    </TabView.Item>
  );
};

export default CommentsTab;

const styles = StyleSheet.create({});
