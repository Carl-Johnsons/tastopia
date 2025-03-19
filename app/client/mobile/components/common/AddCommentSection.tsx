import { Link, router } from "expo-router";
import { Text, View, Image, ActivityIndicator, Alert } from "react-native";
import { TouchableWithoutFeedback } from "react-native-gesture-handler";
import Input from "../Input";
import { useCallback, useState } from "react";
import { Feather } from "@expo/vector-icons";
import { globalStyles } from "./GlobalStyles";
import { useCreateComment } from "@/api/comment";
import { useTranslation } from "react-i18next";
import { selectUser } from "@/slices/user.slice";

type AddCommentSectionProps = {
  recipeId: string;
  setParentState: (comment: CommentType) => void;
};

const AddCommentSection = ({ recipeId, setParentState }: AddCommentSectionProps) => {
  const { t } = useTranslation("recipeDetail");
  const [comment, setComment] = useState("");
  const { mutate: createComment, isLoading } = useCreateComment();
  const { avatarUrl } = selectUser();

  const handleOnSubmit = useCallback(() => {
    if (!comment.trim()) return;
    if (comment.length > 500) {
      Alert.alert(t("commentLimit"));
      return;
    }

    createComment(
      {
        recipeId,
        content: comment.trim()
      },
      {
        onSuccess: data => {
          setParentState(data);
          setComment("");
        },
        onError: error => {
          console.error("Failed to post comment:", error);
          Alert.alert("Fail to create comment");
        }
      }
    );
  }, [comment, recipeId, createComment, setParentState]);
  return (
    <View className='flex-row items-center gap-3'>
      <Image
        source={{ uri: avatarUrl }}
        style={{ width: 24, height: 24, borderRadius: 100 }}
      />

      <Input
        value={comment}
        autoCapitalize='none'
        placeholder={t("addComment")}
        className={`text-black_white flex-1 rounded-3xl border-gray-300 px-2 py-3 focus:border-primary`}
        placeholderTextColor={"gray"}
        onChangeText={setComment}
        editable={!isLoading}
        onSubmitEditing={handleOnSubmit}
        returnKeyType='send'
        multiline={true}
      />

      {comment.trim() !== "" && (
        <TouchableWithoutFeedback
          testID='submit_comment_button'
          onPress={handleOnSubmit}
          disabled={isLoading}
        >
          {isLoading ? (
            <ActivityIndicator
              animating={isLoading}
              color={"white"}
            />
          ) : (
            <Feather
              name='send'
              size={24}
              color={globalStyles.color.primary}
            />
          )}
        </TouchableWithoutFeedback>
      )}
    </View>
  );
};

export default AddCommentSection;
